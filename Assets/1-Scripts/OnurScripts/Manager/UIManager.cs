using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour, IWeaponListener
{
    public int CurrentChargeCount { get; set; }
    [SerializeField] private GameObject[] _chargeUI;
    [SerializeField] private GameObject[] _weaponSwitchUI;
    
  
    private void OnEnable()
    {
        EventLibrary.OnWeaponChargeUpdated.AddListener(OnWeaponChargeLoaded);
        EventLibrary.OnWeaponSwitch.AddListener(HighlightWeaponUI);
    }

    private void OnDisable()
    {
        EventLibrary.OnWeaponChargeUpdated.RemoveListener(OnWeaponChargeLoaded);
        EventLibrary.OnWeaponSwitch.RemoveListener(HighlightWeaponUI);
    }

    public void OnWeaponChargeLoaded(bool isCharged)
    {
        if (isCharged && CurrentChargeCount < 4)
        {
            CurrentChargeCount++;
            _chargeUI[CurrentChargeCount].transform.DOScale(1f, 1f).SetEase(Ease.InBounce);
        }
        else if(CurrentChargeCount >= 1 && !isCharged)
        {
            _chargeUI[CurrentChargeCount].transform.DOScale(0f, 1f).SetEase(Ease.InBounce);
            CurrentChargeCount--;
        }
       
    }

    private void HighlightWeaponUI(ThrowableWeapon weaponIndex)
    {/*
        for (int i = 0; i < _weaponSwitchUI.Length; i++)
            _weaponSwitchUI[i].SetActive(false);

        _weaponSwitchUI[weaponIndex].SetActive(true);
        */
        //revize edilecek
        switch (weaponIndex)
        {
            case Arrow:
                _weaponSwitchUI[0].SetActive(true);
                _weaponSwitchUI[1].SetActive(false);
                break;
            case Bomb:
                _weaponSwitchUI[1].SetActive(true);
                _weaponSwitchUI[0].SetActive(false);
                break;
        }
    }

}
