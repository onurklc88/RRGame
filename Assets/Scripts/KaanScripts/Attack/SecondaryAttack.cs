using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using DG.Tweening;

namespace Test
{
    public class SecondaryAttack : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera vCam;
        [SerializeField] private Transform tempObject;
        [SerializeField] private GameObject player;

        private bool canShot;

        public int speed;

        void Update()
        {
            MouseButtons();

            if (vCam.m_Lens.OrthographicSize >= 11.5f)
                canShot = true;
            else
                canShot = false;
            Debug.Log(canShot);
        }

        private void MouseButtons()
        {
            if (Input.GetMouseButton(1))
                RightClickAttack();

            if (Input.GetMouseButtonUp(1))
            {
                ClearCamSettings();

                if (canShot)
                    Shot();
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

        private void Shot()
        {
            Debug.Log("Shot");
            //GameObject bullet = GameManager.instance.objectPool.GetPooledObject(0);





            canShot = false;

        }
    }
}

