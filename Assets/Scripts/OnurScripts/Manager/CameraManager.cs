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
        CharacterStateManager.SwitchCamAngle += SwitchArrowAngle;
    }

    private void OnDisable()
    {
        CharacterStateManager.SwitchCamAngle -= SwitchArrowAngle;
    }

    private void SwitchArrowAngle(bool started)
    {
        if (started)
        {
            DOTween.To(x => _virtualCamera.m_Lens.OrthographicSize = x, _virtualCamera.m_Lens.OrthographicSize, 12, .5f);
        }
        else
        {
            DOTween.To(x => _virtualCamera.m_Lens.OrthographicSize = x, _virtualCamera.m_Lens.OrthographicSize, 10, .5f);
        }
     
      
        
    }
}
