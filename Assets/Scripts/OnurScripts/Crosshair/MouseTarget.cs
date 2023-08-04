using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTarget : MonoBehaviour
{
    
    
    private LayerMask _groundLayer = 3;
    private static GameObject _depthCamera;
    private static int _layermask;
    private static GameObject _target;

    private void Awake()
    {
        _layermask = (1 << _groundLayer);
        _depthCamera = GameObject.Find("DepthCamera").gameObject;
        _target = GameObject.Find("TargetAim");

    }
    private void Update()
    {
        _target.transform.position = GetMousePosition();
        var direction = GetMousePosition();
        direction.y = 0;
        if (direction == Vector3.zero) return;
        _target.transform.forward = direction;
    }

    public static Vector3 GetMousePosition()
    {
        if (_depthCamera == null) return Vector3.zero;
        
        var ray = _depthCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hitInfo, float.MaxValue, _layermask)) return Vector3.zero;
        var hitPositionIngoredHeight = new Vector3(hitInfo.point.x, 1.08f, hitInfo.point.z);
        return hitPositionIngoredHeight;
    }

  

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(GetMousePosition(), 0.5f);
        Gizmos.DrawLine(Camera.main.transform.forward, GetMousePosition());
    }
}
