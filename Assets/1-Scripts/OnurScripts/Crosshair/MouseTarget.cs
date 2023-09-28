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
    private bool _isAnimating = false;
   


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
        if (_isAnimating)
            return;

        Vector3 charPos;
        if (_isChargingThrowable)
        {
            _target.transform.position = GetMousePosition();
            charPos = _character.transform.position;
        }
        else
        {
            _target.transform.position = GetCursorPosition();
            charPos = GetCharacterPosition();
        }

        _target.transform.LookAt(charPos + GetTargetForward(charPos, _target.transform.position) * 100);
        //RotateTarget(charPos, _target.transform);
    }

    private void RotateTarget(Vector3 charPos, Transform targetTransform)
    {
        var targetPos = targetTransform.position;
        var targetForward = GetTargetForward(charPos, targetPos);
        targetTransform.LookAt(charPos + targetForward * 100);
    }

    private Vector3 GetTargetForward(Vector3 charPos, Vector3 targetPos)
    {
        charPos.y = targetPos.y;
        return targetPos - charPos;
    }
    private Vector3 GetTargetForward(Vector3 charPos, Transform targetTransform)
    {
        var targetPos = targetTransform.position;
        var targetForward = GetTargetForward(charPos, targetPos);
        return Quaternion.LookRotation(targetForward).eulerAngles;
    }

    public void AnimateCharging(bool isStartingToCharge)
    {
        _isAnimating = true;
        var targetTransform = _target.transform;
        if (isStartingToCharge)
        {
            _isChargingThrowable = true;
            var newTargetPos = GetMousePosition();
            var charPos = _character.transform.position;

            targetTransform.DOMove(newTargetPos, 0.1f, false);
            targetTransform.DOLookAt(charPos + GetTargetForward(charPos, newTargetPos) * 100, 0.1f).
                OnComplete(() => _isAnimating = false);
        }
        else
        {
            _isChargingThrowable = false;
            var newTargetPos = GetCursorPosition();
            var charPos = GetCharacterPosition();

            targetTransform.DOMove(newTargetPos, 0.1f, false);
            targetTransform.DOLookAt(charPos + GetTargetForward(charPos, newTargetPos) * 100, 0.1f).
                OnComplete(() => _isAnimating = false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(GetMousePosition(), 0.5f);
        Gizmos.DrawLine(Camera.main.transform.position, GetMousePosition());
    }
}
