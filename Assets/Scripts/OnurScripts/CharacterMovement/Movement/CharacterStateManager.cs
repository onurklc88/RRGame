using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;



[RequireComponent(typeof(CharacterController))]
public class CharacterStateManager : MonoBehaviour
{
    public static event Action<bool> SwitchCamAngle;
    [SerializeField] private CharacterProperties _characterProperties;
    [SerializeField] private ObjectPool _objectPool;
    [HideInInspector] public CharacterController CharacterController;
    [HideInInspector] public bool IsMovementPressed;
    [HideInInspector] public bool IsSlidePressed = false;
  
   
    private CharacterStateFactory _characterStateFactory = new CharacterStateFactory();
    private Vector3 _currentMovement;
    private Vector3 _positionToLookAt;
    #region Getters & Setters
    //getter and Setters
    public CharacterProperties CharacterProperties => _characterProperties;
    public float CharacterSpeed => _characterProperties.WalkSpeed;
    //public CharacterAttackState.AttackType AttackType => _attackType;
    public Vector3 CurrentMove => _currentMovement;
    public CharacterStateFactory CharacterStateFactory => _characterStateFactory;
    public ObjectPool ObjectPool => _objectPool;
    public Vector3 PositionToLookAt => _positionToLookAt;

    #endregion
    private PlayerInput _playerInput;
    private Vector2 _readVector;
    private float _rotationFactorPerFrame = 15f;
    private float _buttonPressedTime = 3f;
    private CharacterBaseState _currentState = null;
   

    private void OnEnable()
    {
        CharacterHealth.PlayerKnockbackEvent += SwitchState;
        _playerInput.CharacterControls.Enable();
    }
    private void OnDisable()
    {
        CharacterHealth.PlayerKnockbackEvent -= SwitchState;
        _playerInput.CharacterControls.Disable();
    }

    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        ReadInputs();
    }
    private void Start()
    {
        _currentState = _characterStateFactory.CharacterIdleState;
        _currentState.EnterState(this);
    }

    private void Update()
    {
        HandleRotation();
        HandleGravity();
        _currentState.UpdateState(this);
    }

    #region Inputs and movements
    private void ReadInputs()
    {
        _playerInput = new PlayerInput();
        _playerInput.CharacterControls.Move.started += OnMovementInput;
        _playerInput.CharacterControls.Move.canceled += OnMovementInput;
        _playerInput.CharacterControls.Move.performed += OnMovementInput;
        _playerInput.CharacterControls.Slide.started += OnSlideMovement;
        _playerInput.CharacterControls.MeleeAttack.performed += OnMeleeAttackStarted;
        _playerInput.CharacterControls.MeleeAttack.canceled += OnMeleeAttackEnded;
        _playerInput.CharacterControls.LongRangeAttack.started += OnLongRangeAttackStarted;
        _playerInput.CharacterControls.LongRangeAttack.canceled += OnLongRangeAttackEnded;
    }

    private void OnSlideMovement(InputAction.CallbackContext context)
    {
        //timer eklenecek
        IsSlidePressed = context.ReadValueAsButton();
        SwitchState(_characterStateFactory.CharacterSlideState);
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        _readVector = context.ReadValue<Vector2>();
        Vector3 toConvert = new Vector3(_readVector.x, 0, _readVector.y);
        _currentMovement = IsoVectorToConvert(toConvert);
        if(_currentState == _characterStateFactory.CharacterAttackState)
             IsMovementPressed = false;
        else
            IsMovementPressed = _readVector.x != 0 || _readVector.y != 0;
    }
    
    private void OnMeleeAttackStarted(InputAction.CallbackContext context)
    {
        if (_currentState == _characterStateFactory.CharacterSlideState || _characterStateFactory.CurrentCombatType == CharacterStateFactory.CombatType.LongRange) return;
        _characterStateFactory.CurrentCombatType = CharacterStateFactory.CombatType.Melee;
        _buttonPressedTime = Time.time;
    }

    private void OnMeleeAttackEnded(InputAction.CallbackContext context)
    {
        float heldTime = Time.time - _buttonPressedTime;
       
        if (heldTime < 1f)
           _characterStateFactory.CharacterAttackState = _characterStateFactory.LightAttack;
        else
           _characterStateFactory.CharacterAttackState = _characterStateFactory.HeavyAttack;
        
        SwitchState(_characterStateFactory.CharacterAttackState);
    }

    private void OnLongRangeAttackStarted(InputAction.CallbackContext context)
    {
        if (_currentState == _characterStateFactory.CharacterSlideState || _characterStateFactory.CurrentCombatType == CharacterStateFactory.CombatType.Melee) return;

        _characterStateFactory.CurrentCombatType = CharacterStateFactory.CombatType.LongRange;
        SwitchCamAngle?.Invoke(true);
        _characterStateFactory.CharacterAttackState = _characterStateFactory.ThrowBomb;
        SwitchState(_characterStateFactory.CharacterAttackState);
    }
    private void OnLongRangeAttackEnded(InputAction.CallbackContext context)
    {
        SwitchCamAngle?.Invoke(false);
        _characterStateFactory.CharacterAttackState.AttackBehaviour(this);
    }
    
    private Vector3 IsoVectorToConvert(Vector3 vector)
    {
        Quaternion rotation = Quaternion.Euler(0, 45.0f, 0f);
        Matrix4x4 isoMatrix = Matrix4x4.Rotate(rotation);
        Vector3 result = isoMatrix.MultiplyPoint3x4(vector);
        return result;
    }

    #endregion
    private void HandleGravity()
    {
        if (CharacterController.isGrounded)
        {
            float groundedGravity = -0.5f;
            _currentMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -0.5f;
            _currentMovement.y += gravity;
            CharacterController.SimpleMove(new Vector3(transform.position.x, _currentMovement.y, transform.position.z));
        }
    }

    private void HandleRotation()
    {
        if (_currentState == _characterStateFactory.CharacterAttackState || IsSlidePressed) return;
        
        _positionToLookAt.x = _currentMovement.x;
        _positionToLookAt.y = 0f;
        _positionToLookAt.z = _currentMovement.z;
        Quaternion currentRotation = transform.rotation;

       
        if (!IsMovementPressed) return;
        
            Quaternion targetRotation = Quaternion.LookRotation(_positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationFactorPerFrame * Time.deltaTime);
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




