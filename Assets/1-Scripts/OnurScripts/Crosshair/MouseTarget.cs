using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseTarget : MonoBehaviour
{
    [SerializeField] private Camera _depthCamera;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _character;
    [SerializeField] private GameObject _target;

    public LayerMask _targetLayer;
    private LayerMask _groundLayer = 3;
    private int _layerMask;
    private bool _isChargingThrowable = false;
    private bool _isStartingToCharge = false;
    private bool _isAnimating = false;

    private const float ANIMATION_TIME = 0.1f;
    private float _animationTimeElapsed = 0;

    public Vector3 MousePosition { get; private set; }

    private void Awake()
    {
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Confined;
        _layerMask = (1 << _groundLayer);
    }
    private void Update()
    {
        CursorMovement();
    }
   
   

    public Vector3 GetMousePosition()
    {
        if (_depthCamera == null) return Vector3.zero;

        var ray = _depthCamera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hitInfo, float.MaxValue, _layerMask)) return Vector3.zero;
        var hitPositionIngoredHeight = new Vector3(hitInfo.point.x, _character.transform.position.y, hitInfo.point.z);
        return hitPositionIngoredHeight;
    }

    public Vector3 GetCursorPosition()
    {
        var ray = _depthCamera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hitInfo, float.MaxValue, _targetLayer)) return Vector3.zero;
        var hitPositionIngoredHeight = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
        return hitPositionIngoredHeight;
    }

    private Vector3 GetCharacterPosition()
    {
        var charScreenPos = _mainCamera.WorldToScreenPoint(_character.transform.position);
        var ray = _depthCamera.ScreenPointToRay(charScreenPos);
        if (!Physics.Raycast(ray, out var hitInfo, float.MaxValue, _targetLayer)) return Vector3.zero;
        var hitPositionIngoredHeight = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
        return hitPositionIngoredHeight;
    }

    private void CursorMovement()
    {
        MousePosition = GetMousePosition();

        if (_isAnimating)
        {
            _isAnimating = AnimationRoutine();
            if (_isAnimating)
            {
                return;
            }
        }

        if (_isChargingThrowable)
        {
            _target.transform.position = MousePosition;
        }
        else
        {
            _target.transform.position = GetCursorPosition();
        }
    }
  
    // returns true if animation should continue
    private bool AnimationRoutine()
    {
        if (_animationTimeElapsed < ANIMATION_TIME)
        {
            _animationTimeElapsed += Time.deltaTime;
            AnimateCharging(_isChargingThrowable, _animationTimeElapsed);
            return true;
        }
        else
        {
            _animationTimeElapsed = 0;
            return false;
        }
    }

    private void AnimateCharging(bool isStartingToCharge, float timeElapsed)
    {
        var cursorPos = GetCursorPosition();
        float lerpValue = isStartingToCharge ? timeElapsed : ANIMATION_TIME - timeElapsed;
        lerpValue /= ANIMATION_TIME;

        var targetPos = Vector3.Lerp(cursorPos, MousePosition, lerpValue);
        _target.transform.position = targetPos;
    }

    public void AnimateCharging(bool isStartingToCharge)
    {
        _isAnimating = true;
        _isChargingThrowable = isStartingToCharge;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(MousePosition, 0.5f);
        Gizmos.DrawLine(Camera.main.transform.position, MousePosition);
    }
}
