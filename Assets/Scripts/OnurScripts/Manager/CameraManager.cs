using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
   

    private void OnEnable()
    {
        EventLibrary.OnLongRangeAttack.AddListener(SwitchAttackCameraAngle);
    }

    private void OnDisable()
    {
        EventLibrary.OnLongRangeAttack.RemoveListener(SwitchAttackCameraAngle);
   }

    private void SwitchAttackCameraAngle(bool started)
    {
        /*
        if (started)
        {
            DOTween.To(x => _virtualCamera.m_Lens.OrthographicSize = x, _virtualCamera.m_Lens.OrthographicSize, 20, .5f);
        }
        else
        {
            DOTween.To(x => _virtualCamera.m_Lens.OrthographicSize = x, _virtualCamera.m_Lens.OrthographicSize, 15.1f, .5f);
        }
        */
    }

  
}
