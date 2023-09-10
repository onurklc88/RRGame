using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using Zenject;
using Cinemachine;


[RequireComponent(typeof(CharacterController))]
public class CharacterStateManager : MonoBehaviour
{
    #region Getters & Setters
    public CharacterProperties CharacterProperties => _characterProperties;
    public MouseTarget MouseTarget => _mouseTarget;
    public float CharacterSpeed => _characterProperties.WalkSpeed;
    public Vector3 CurrentMove => _currentMovement;
    public WeaponHandler WeaponHandler => _weaponHandler;
    public Vector3 PositionToLookAt => _positionToLookAt;
    public CharacterContainer CharacterContainer => _characterContainer;
    public CinemachineImpulseSource ImpulseSource => _impulseSource;
    #endregion
    [HideInInspector] public CharacterController CharacterController;
    public bool IsMovementPressed { get; set; }
    public bool IsSlidePressed { get; set; }
    [SerializeField] private CharacterProperties _characterProperties;
    private CharacterContainer _characterContainer;
    [SerializeField] private CinemachineImpulseSource _impulseSource;
  
    #region Depency Injections
    [Inject]
    MouseTarget _mouseTarget;
    [Inject]
    CharacterCollisions _characterCollisions;
    [Inject]
    public CharacterStateFactory CharacterStateFactory;
    [Inject]
    WeaponHandler _weaponHandler;
    #endregion
    private CharacterBaseState _currentState = null;
    private Vector3 _currentMovement;
    private Vector3 _positionToLookAt;
    private PlayerInput _playerInput;
    private Vector2 _readVector;
    private float _rotationFactorPerFrame = 15f;
    private bool _canCharacterSlide = true;
    private float _gravity = -0.5f;
   
    private void OnEnable()
    {
        EventLibrary.OnPlayerTakeDamage.AddListener(SwitchState);
        _playerInput.CharacterControls.Enable();
    }
    private void OnDisable()
    {
        EventLibrary.OnPlayerTakeDamage.RemoveListener(SwitchState);
        _playerInput.CharacterControls.Disable();
    }

    private void Awake()
    {
        _characterContainer = GetComponent<CharacterContainer>();
        CharacterController = GetComponent<CharacterController>();
        CharacterStateFactory.CharacterAttackState = null;
        ReadInputs();
    }
    private void Start()
    {
        _currentState = CharacterStateFactory.CharacterIdleState;
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
        _playerInput.CharacterControls.MeleeAttack.started += OnMeleeAttackStarted;
        _playerInput.CharacterControls.MeleeAttack.canceled += OnMeleeAttackEnded;
        _playerInput.CharacterControls.LongRangeAttack.started += OnLongRangeAttackStarted;
        _playerInput.CharacterControls.LongRangeAttack.canceled += OnLongRangeAttackEnded;
        _playerInput.CharacterControls.Interaction.started += OnPlayerInteraction;
    }

    private void OnSlideMovement(InputAction.CallbackContext context)
    {
        if (!_canCharacterSlide) return;
        _canCharacterSlide = false;
        IsSlidePressed = context.ReadValueAsButton();
        SwitchState(CharacterStateFactory.CharacterSlideState);
        DOVirtual.DelayedCall(0.7f, () =>{ _canCharacterSlide = true;});
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        _readVector = context.ReadValue<Vector2>();
        Vector3 toConvert = new Vector3(_readVector.x, 0, _readVector.y);
        _currentMovement = IsoVectorToConvert(toConvert);
       
        if (_currentState == CharacterStateFactory.CharacterAttackState)
             IsMovementPressed = false;
        else
            IsMovementPressed = _readVector.x != 0 || _readVector.y != 0;
    }
    private void OnMeleeAttackStarted(InputAction.CallbackContext context)
    {
        if (CharacterStateFactory.CharacterAttackState != null || _currentState == CharacterStateFactory.CharacterAttackState) return;
       CharacterStateFactory.CharacterAttackState = CharacterStateFactory.LightAttack; 
    }
    private void OnMeleeAttackEnded(InputAction.CallbackContext context)
    {
       if (_currentState == CharacterStateFactory.CharacterSlideState || _currentState == CharacterStateFactory.CharacterAttackState) return;

       if (context.duration < 0.5f)
            CharacterStateFactory.CharacterAttackState = CharacterStateFactory.LightAttack;
        else
            CharacterStateFactory.CharacterAttackState = CharacterStateFactory.HeavyAttack;
        
        SwitchState(CharacterStateFactory.CharacterAttackState);
        CharacterStateFactory.CharacterAttackState = null;
    }

    private void OnLongRangeAttackStarted(InputAction.CallbackContext context)
    {
       if (_currentState == CharacterStateFactory.CharacterSlideState || CharacterStateFactory.CharacterAttackState != null) return;
        
        EventLibrary.OnLongRangeAttack.Invoke(true);
        switch (WeaponHandler.HandedWeapon())
        {
            case Arrow:
                CharacterStateFactory.CharacterAttackState = CharacterStateFactory.ThrowArrow;
                break;
            case Bomb:
                CharacterStateFactory.CharacterAttackState = CharacterStateFactory.ThrowBomb;
                break;
        }
        SwitchState(CharacterStateFactory.CharacterAttackState);
    }
    private void OnLongRangeAttackEnded(InputAction.CallbackContext context)
    {
        if (_currentState == CharacterStateFactory.CharacterSlideState || CharacterStateFactory.CharacterAttackState == CharacterStateFactory.LightAttack || CharacterStateFactory.CharacterAttackState == null) return;
        EventLibrary.OnLongRangeAttack.Invoke(false);
        if (context.duration > 0.3)
            CharacterStateFactory.CharacterAttackState.DoAttackBehaviour(this);
        else
            CharacterStateFactory.CharacterAttackState.ExitState(this);
        
        CharacterStateFactory.CharacterAttackState = null;
    }

    private void OnPlayerInteraction(InputAction.CallbackContext context)
    {
        if(_characterCollisions.TemporaryObject != null) { SwitchState(CharacterStateFactory.CharacterClimbState); }
    }
   
    private Vector3 IsoVectorToConvert(Vector3 vector)
    {
        Quaternion rotation = Quaternion.Euler(0, 45.0f, 0f);
        Matrix4x4 isoMatrix = Matrix4x4.Rotate(rotation);
        Vector3 result = isoMatrix.MultiplyPoint3x4(vector);
        return result;
    }

    #endregion
    public void HandleGravity()
    {
        if (CharacterController.isGrounded)
        {
            _currentMovement.y = _gravity;
        }
        else
        {
            _currentMovement.y += _gravity;
            CharacterController.SimpleMove(new Vector3(transform.position.x, _currentMovement.y, transform.position.z));
        }
    }

    private void HandleRotation()
    {
        if (_currentState == CharacterStateFactory.LightAttack || _currentState == CharacterStateFactory.CharacterSlideState || _currentState == CharacterStateFactory.HeavyAttack || _currentState == CharacterStateFactory.CharacterClimbState) return;
        
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
        Gizmos.DrawWireSphere(transform.position + transform.forward * 2f, 2f);
   }
}




