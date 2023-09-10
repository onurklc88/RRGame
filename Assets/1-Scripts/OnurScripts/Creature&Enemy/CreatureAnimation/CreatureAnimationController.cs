using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CreatureAnimationController : MonoBehaviour
{
    public bool AlreadyWalked { get; set; }
   [SerializeField] private Animator _creatureAnimation;
   private int _lerpA;
   private int _lerpB;
   private float _velocity = 0f;
   private float _duration = 0.2f;
   private int _velocityHash;
    private void Start()
    {
        _velocityHash = Animator.StringToHash("WalkMotion");
    }
  

    public void PlayBlendAnimations(bool movementStart)
    {
        if (movementStart)
        {
            _lerpA = 0;
            _lerpB = 1;
            AlreadyWalked = true;
        }
        else
        {
            _lerpA = 1;
            _lerpB = 0;
            AlreadyWalked = false;
        }
        StartCoroutine(HandleAnimatonTreshold(_lerpA, _lerpB));
    }
    private IEnumerator HandleAnimatonTreshold(int lerpA, int lerpB)
    {

        float elapsedTime = 0f;
        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            _velocity = Mathf.Lerp(lerpA, lerpB, elapsedTime / _duration);

            _creatureAnimation.SetFloat(_velocityHash, _velocity);
            yield return null;
        }

    }

    public void PlayCreatureAnimation(string animatonState, bool tempValue)
    {
        _creatureAnimation.Play(animatonState);
       // _creatureAnimation.SetBool(animatonState, tempValue);
    }
}
