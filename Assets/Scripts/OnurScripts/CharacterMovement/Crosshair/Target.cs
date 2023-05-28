using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;


public class Target : MonoBehaviour
{
    [SerializeField] private Transform aim;
    [SerializeField] private Image image;

    private void Update()
    {
        Ray ray = MousePointer.GetWorldRay(Camera.main);
        aim.transform.position =  ray.origin;
        Debug.Log(ray);
        //image.t = MousePointer.GetBoundedScreenPosition();
    }
}
