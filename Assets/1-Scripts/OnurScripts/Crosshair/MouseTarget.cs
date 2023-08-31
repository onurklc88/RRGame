using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseTarget : MonoBehaviour
{
    [SerializeField] private GameObject _depthCamera;
    [SerializeField] private GameObject _character;
    [SerializeField] private GameObject _target;
    public LayerMask _targetLayer;
    private LayerMask _groundLayer = 3;
    private int _layerMask;
  
   

    

    private void Awake()
    {
        Cursor.visible = false;
        _layerMask = (1 << _groundLayer);
    }
    private void Update()
    {
        _target.transform.position = GetPos();
    }


  
    public Vector3 GetMousePosition()
    {
        if (_depthCamera == null) return Vector3.zero;
        
        var ray = _depthCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hitInfo, float.MaxValue, _layerMask)) return Vector3.zero;
        var hitPositionIngoredHeight = new Vector3(hitInfo.point.x, _character.transform.position.y, hitInfo.point.z);
        return hitPositionIngoredHeight;
    }

    private Vector3 GetPos()
    {
        var ray = _depthCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hitInfo, float.MaxValue, _targetLayer)) return Vector3.zero;
        var hitPositionIngoredHeight = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
        return hitPositionIngoredHeight;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(GetMousePosition(), 0.5f);
        Gizmos.DrawLine(Camera.main.transform.position, GetMousePosition());
    }
}
