using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using DG.Tweening;

public class SecondaryAttack : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private Transform tempObject;
    [SerializeField] private GameObject player;
 
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetMouseButton(1))
            RightClickAttack();
        if (Input.GetMouseButtonUp(1))
        {
            ClearCamSettings();
        }
    }

    private void RightClickAttack()
    {
        DOTween.To(x => vCam.m_Lens.OrthographicSize = x, vCam.m_Lens.OrthographicSize, 12, .5f);
        vCam.Follow = tempObject;
    }

    private void ClearCamSettings()
    {
        DOTween.To(x => vCam.m_Lens.OrthographicSize = x, vCam.m_Lens.OrthographicSize, 10, .5f);

        vCam.Follow = player.transform;
    }
}
