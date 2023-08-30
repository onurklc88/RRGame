using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour, IWeaponListener
{
    public int CurrentChargeCount { get; set; }
    [SerializeField] private GameObject[] _chargeUI;
  
    private void OnEnable()
    {
        EventLibrary.OnWeaponChargeUpdated.AddListener(OnWeaponChargeLoaded);
    }

    private void OnDisable()
    {
        EventLibrary.OnWeaponChargeUpdated.RemoveListener(OnWeaponChargeLoaded);
    }

    private void Start()
    {
        CurrentChargeCount = -1;
    }

    public void OnWeaponChargeLoaded(bool isCharged)
    {
        if (isCharged && CurrentChargeCount < 4)
        {
            CurrentChargeCount++;
            _chargeUI[CurrentChargeCount].transform.DOScale(1f, 1f).SetEase(Ease.InBounce);
        }
        else if(CurrentChargeCount >= 0 && !isCharged)
        {
            _chargeUI[CurrentChargeCount].transform.DOScale(0f, 1f).SetEase(Ease.InBounce);
            CurrentChargeCount--;
        }
    
    }

}
