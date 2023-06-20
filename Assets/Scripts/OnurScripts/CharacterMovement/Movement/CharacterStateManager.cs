using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;


[RequireComponent(typeof(CharacterController))]
public class CharacterStateManager : MonoBehaviour
{

    [SerializeField] private CharacterProperties _characterProperties;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [HideInInspector] public CharacterController CharacterController;
    [HideInInspector] public bool IsMovementPressed;
    [HideInInspector] public bool IsSlidePressed = false;
    [HideInInspector] public bool IsAtackPressed;
    private CharacterAttackState.AttackType _attackType;
    private CharacterStateFactory _characterStateFactory = new CharacterStateFactory();
    public Vector3 _currentMovement;
    public Vector3 DashPosition;
    private Vector3 _positionToLookAt;
    #region Getters & Setters
    //getter and Setters
    public CinemachineVirtualCamera VirtualCamera => _virtualCamera;
    public CharacterProperties CharacterProperties => _characterProperties;
    public float CharacterSpeed => _characterProperties.WalkSpeed;
    public CharacterAttackState.AttackType AttackType => _attackType;
    public Vector3 CurrentMove => _currentMovement;
    public CharacterStateFactory CharacterStateFactory => _characterStateFactory;

    public Vector3 PositionToLookAt => _positionToLookAt;

    #endregion

    
    private PlayerInput _playerInput;
    private Vector2 _readVector;
    private float _rotationFactorPerFrame = 15f;
    private float _buttonPressedTime = 3f;
    private CharacterBaseState _currentState = null;
   

    private void OnEnable()
    {
        _characterStateFactory.CharacterAttackState.AddActionTypes(this);
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
        _playerInput = new PlayerInput();
        CharacterController = GetComponent<CharacterController>();
       
        _currentState = _characterStateFactory.CharacterIdleState;
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
        HandleRotation();
        HandleGravity();
        _currentState.UpdateState(this);
    }

    #region Inputs and movements
    private void OnSlideMovement(InputAction.CallbackContext context)
    {

        //slide movement tek x yönünde rotasyon olarak hareket edecek
        if (_currentState == _characterStateFactory.CharacterSlideState) return;
        IsSlidePressed = context.ReadValueAsButton();
        SwitchState(_characterStateFactory.CharacterSlideState);
    }


    private void OnMovementInput(InputAction.CallbackContext context)
    {
        //bu ýnputlarý slide anýnda disable etmemiz gerekiyor
        _readVector = context.ReadValue<Vector2>();
        Vector3 toConvert = new Vector3(_readVector.x, 0, _readVector.y);
        _currentMovement = IsoVectorToConvert(toConvert);
        IsMovementPressed = _readVector.x != 0 || _readVector.y != 0;
    }
    
    private void OnAttackStarted(InputAction.CallbackContext context)
    {
        _buttonPressedTime = Time.time;
    }


    private void OnAttackEnded(InputAction.CallbackContext context)
    {
        float heldTime = Time.time - _buttonPressedTime;

        if (heldTime < 0.5f)
            _attackType = CharacterAttackState.AttackType.Light;
        else
            _attackType = CharacterAttackState.AttackType.Heavy;

        if (_currentState != _characterStateFactory.CharacterSlideState)
        {
            SwitchState(_characterStateFactory.CharacterAttackState);
        }


    }
    private bool IsMovingUpSlope(Vector3 slopeNormal)
    {
        float dot = Vector3.Dot(slopeNormal, _currentMovement.normalized);
        return dot > 0f;
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




