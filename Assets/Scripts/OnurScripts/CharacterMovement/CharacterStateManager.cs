using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class CharacterStateManager : MonoBehaviour
{
   private PlayerInput _playerInput;
   [HideInInspector] public CharacterController CharacterController;
   [HideInInspector] public bool IsMovementPressed;
  
   [HideInInspector] public Vector3 _currentMovement;
   private Vector2 _currentMovementInput;
   private float _rotationFactorPerFrame = 15f;
    
    
    
    #region CharacterStates
    private CharacterBaseState _currentState = null;
    public CharacterIdleState CharacterIdleState = new CharacterIdleState();
    public CharacterWalkState CharacterWalkState = new CharacterWalkState();
    public CharacterSlipState CharacterSlipState = new CharacterSlipState();
    public CharacterClimbState CharacterClimbState = new CharacterClimbState();
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
        _playerInput.CharacterControls.Slide.canceled += OnSlideMovement;
    }

   
    private void Update()
    {
        HandleGravity();
        HandleRotation();
        _currentState.UpdateState(this);
    }
   

    private void HandleRotation()
    {
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


}
