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
    private bool asd;

   


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
        var hitPositionIngoredHeight = new Vector3(hitInfo.point.x, -2.158f, hitInfo.point.z);
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
        _target.transform.position = GetMousePosition();
        var charPos = GetCharacterPosition();
        var lookAtVector = _target.transform.position - charPos;
      //  _target.transform.LookAt(_target.transform.position + lookAtVector);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(GetMousePosition(), 0.5f);
        Gizmos.DrawLine(Camera.main.transform.position, GetMousePosition());
    }
}
