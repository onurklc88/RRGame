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
        EventLibrary.OnWeaponDestroy.AddListener(CameraShake);
       
    }

    private void OnDisable()
    {
        EventLibrary.OnLongRangeAttack.RemoveListener(SwitchAttackCameraAngle);
        EventLibrary.OnWeaponDestroy.RemoveListener(CameraShake);

    }

    private void SwitchAttackCameraAngle(bool started)
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

    private void CameraShake(CinemachineImpulseSource source)
    {
    
        switch (source.name)
        {
            case "Arrow(Clone)":
               source.GenerateImpulseWithForce(1f);
                break;
            case "Bomb(Clone)":
                source.GenerateImpulseWithForce(0.5f); 
                break;
        }
        
    }
}
