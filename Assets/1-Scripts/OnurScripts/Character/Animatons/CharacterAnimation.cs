using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class CharacterAnimation : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Animator _characterAnimator;
    private float _velocity = 0f;
    private float _duration = 0.2f;
    private int _velocityHash;
    private int _lerpA;
    private int _lerpB;
    
    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
        EventLibrary.StartRunAnimation.AddListener(PlayRunAnimation);
        EventLibrary.PlayDashAnimation.AddListener(PlaySlideAnimation);
        EventLibrary.PlayAttackAnimation.AddListener(PlayAttackAnimation);
        EventLibrary.OnPlayerThrowBomb.AddListener(PlayThrowBombAnimation);
        
       
    }
    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
        EventLibrary.StartRunAnimation.RemoveListener(PlayRunAnimation);
        EventLibrary.PlayDashAnimation.RemoveListener(PlaySlideAnimation);
        EventLibrary.PlayAttackAnimation.RemoveListener(PlayAttackAnimation);
        EventLibrary.OnPlayerThrowBomb.RemoveListener(PlayThrowBombAnimation);
    }
    private void Awake()
    {
        ReadInput();
    }


    private void Start()
    {
        
        _characterAnimator = GetComponent<Animator>();
        _velocityHash = Animator.StringToHash("WalkVelocity");
    }

    private void ReadInput()
    {
        
        _playerInput = new PlayerInput();
      
       
        
       
    }

    private void PlayRunAnimation(bool isMovementStart)
    {
        if (isMovementStart)
        {
            _lerpA = 0;
            _lerpB = 1;
        }
        else
        {
            _lerpA = 1; 
            _lerpB = 0;
        }
         
        StartCoroutine(HandleAnimatonTreshold(_lerpA, _lerpB));
    }

    private void PlaySlideAnimation(bool tempValue)
    {
       _characterAnimator.SetBool("IsDashing", tempValue);
    }

    private void PlayAttackAnimation(bool tempValue)
    {
        _characterAnimator.SetBool("IsAttacking", tempValue);
    }

    private void PlayThrowBombAnimation(bool tempValue)
    {
      
       
            _characterAnimator.SetBool("IsThrowStart", tempValue);
       
         
    }


    private IEnumerator HandleAnimatonTreshold(int lerpA, int lerpB)
    {
       
        float elapsedTime = 0f;
        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            _velocity = Mathf.Lerp(lerpA, lerpB, elapsedTime / _duration);
            
            _characterAnimator.SetFloat(_velocityHash, _velocity);
            yield return null;
        }
      
    }

    private IEnumerator Example(int lerpA, int lerpB)
    {
        _characterAnimator.SetBool("IsDashing", true);
        float elapsedTime = 0f;
        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            _velocity = Mathf.Lerp(lerpA, lerpB, elapsedTime / _duration);
            Debug.Log("Velocity: " + _velocity);
            _characterAnimator.SetFloat(_velocityHash, _velocity);
            yield return null;
        }
        _characterAnimator.SetBool("IsDashing", false);
    }
}
