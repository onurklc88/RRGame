using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(CharacterController))]
public class CharacterStateManager : MonoBehaviour
{

    [SerializeField] private CharacterProperties _characterProperties;
    [HideInInspector] public CharacterController CharacterController;
    [HideInInspector] public bool IsMovementPressed;
    [HideInInspector] public bool IsSlidePressed;
    [HideInInspector] public bool IsAtackPressed;
    [HideInInspector] public Vector3 _currentMovement;

   
    private bool _heavyAttack;

    //getter and Setters

    public CharacterProperties CharacterProperties => _characterProperties;
    public float CharacterSpeed => _characterProperties.WalkSpeed;

    public bool HeavyAttack => _heavyAttack;


    private PlayerInput _playerInput;
    private Vector2 _currentMovementInput;
    private float _rotationFactorPerFrame = 15f;
    private float _buttonPressedTime = 3f;



    #region CharacterStates
    private CharacterBaseState _currentState = null;
    public CharacterIdleState CharacterIdleState = new CharacterIdleState();
    public CharacterWalkState CharacterWalkState = new CharacterWalkState();
    public CharacterSlideState CharacterSlideState = new CharacterSlideState();
    public CharacterClimbState CharacterClimbState = new CharacterClimbState();
    public CharacterAttackState CharacterAttackState = new CharacterAttackState();
    #endregion

    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }
    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }

    private void Awake()
    {
        _playerInput = new PlayerInput();
        CharacterController = GetComponent<CharacterController>();
        _currentState = CharacterIdleState;
        _currentState.EnterState(this);
        _playerInput.CharacterControls.Move.started += OnMovementInput;
        _playerInput.CharacterControls.Move.canceled += OnMovementInput;
        _playerInput.CharacterControls.Move.performed += OnMovementInput;
        _playerInput.CharacterControls.Slide.started += OnSlideMovement;
        _playerInput.CharacterControls.Attack.performed += OnAttackStarted;
        _playerInput.CharacterControls.Attack.canceled += OnAttackEnded;
    }


    private void Update()
    {
        HandleGravity();
        HandleRotation();
        _currentState.UpdateState(this);
    }


    private void HandleRotation()
    {
        if (IsSlidePressed) return;

        Vector3 positionToLookAt;
        positionToLookAt.x = _currentMovement.x;
        positionToLookAt.y = 0f;
        positionToLookAt.z = _currentMovement.z;
        Quaternion currentRotation = transform.rotation;
        if (IsMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationFactorPerFrame * Time.deltaTime);
        }
    }

    private void OnAttackStarted(InputAction.CallbackContext context)
    {
        _buttonPressedTime = Time.time;
    }


    private void OnAttackEnded(InputAction.CallbackContext context)
    {
        float heldTime = Time.time - _buttonPressedTime;

        if (heldTime < 0.5f)
         _heavyAttack = false;
        else
         _heavyAttack = true;
         
       if(_currentState != CharacterSlideState)
       {
            SwitchState(CharacterAttackState);
       }


    }

    private void HandleGravity()
    {
        if (CharacterController.isGrounded)
        {
            float groundedGravity = -0.01f;
            _currentMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -2.8f;
            _currentMovement.y += gravity;
        }
    }

    private void OnSlideMovement(InputAction.CallbackContext context)
    {
        IsSlidePressed = context.ReadValueAsButton();
        SwitchState(CharacterSlideState);
    }


    private void OnMovementInput(InputAction.CallbackContext context)
    {
        
        _currentMovementInput = context.ReadValue<Vector2>();
        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.z = _currentMovementInput.y;
        IsMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
      
    }

    public void SwitchState(CharacterBaseState newState)
    {
        _currentState = newState;
        _currentState.EnterState(this);
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * 2f, 1.5f);
    }
}




