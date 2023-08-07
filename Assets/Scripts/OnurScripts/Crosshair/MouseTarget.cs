using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTarget : MonoBehaviour
{

    private LayerMask _slopeLayer = 9;
    private LayerMask _groundLayer = 3;
    private static GameObject _depthCamera;
    private static int _layerMask;
    private static int _slopeMask;
    private static GameObject _target;
    public static GameObject Character;

    private void Awake()
    {
        Cursor.visible = false;
        _layerMask = (1 << _groundLayer);
        _slopeMask = (1 << _slopeLayer);
        _depthCamera = GameObject.Find("DepthCamera").gameObject;
        _target = GameObject.Find("TargetAim");
        Character = GameObject.Find("Character");
    }
    private void Update()
    {
        _target.transform.position = new Vector3(GetMousePosition().x, Character.transform.position.y, GetMousePosition().z);
        
       
    }

    public static Vector3 GetMousePosition()
    {
        if (_depthCamera == null) return Vector3.zero;
        
        var ray = _depthCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hitInfo, float.MaxValue, _layerMask)) return Vector3.zero;
        var hitPositionIngoredHeight = new Vector3(hitInfo.point.x, Character.transform.position.y, hitInfo.point.z);
        return hitPositionIngoredHeight;
    }

    public static Vector3 GetMousePositionOnSlope()
    {
        if (_depthCamera == null) return Vector3.zero;
        var ray = _depthCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hitInfo, float.MaxValue, _slopeMask)) return Vector3.zero;
        var hitPositionIngoredHeight = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
        return hitPositionIngoredHeight;
    }

  

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(GetMousePosition(), 0.5f);
        Gizmos.DrawLine(Camera.main.transform.position, GetMousePositionOnSlope());
    }
}
