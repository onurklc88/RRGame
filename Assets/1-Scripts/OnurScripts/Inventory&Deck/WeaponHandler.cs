using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Zenject;


public class WeaponHandler : MonoBehaviour, IWeaponListener
{
    
    private PlayerInput _playerInput;
    [Inject]
    Weapons _weapons;
    private ThrowableWeapon _currentWeapon;
    private int _totalCharge = 4;
    private int _currentChargeCount = 0;
    private Dictionary<string, ThrowableWeapon> _weaponDictionary = new Dictionary<string, ThrowableWeapon>();
    public int CurrentChargeCount { get; set; }
    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
        EventLibrary.OnWeaponChargeUpdated.AddListener(OnWeaponChargeLoaded);
    }
    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
        EventLibrary.OnWeaponChargeUpdated.RemoveListener(OnWeaponChargeLoaded);
    }

    private void Start()
    {
        _currentWeapon = _weapons.Arrow;
        _weaponDictionary.Add("Arrow", _weapons.Arrow);
        _weaponDictionary.Add("Bomb", _weapons.Bomb);
    }
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.CharacterControls.Arrow.started += OnWeaponInput;
        _playerInput.CharacterControls.Bomb.started += OnWeaponInput;
    }

    private void OnWeaponInput(InputAction.CallbackContext context)
    {
      _currentWeapon = _weaponDictionary[context.action.name];
    }
    public void OnWeaponChargeLoaded(bool isCharged)
    {
       
        if (isCharged && CurrentChargeCount < _totalCharge)
            CurrentChargeCount++;
        else if(CurrentChargeCount >= 0)
            CurrentChargeCount--;

       
    }

    public ThrowableWeapon HandedWeapon()
    {
        return _currentWeapon;
    }

    public bool IsChargeReady()
    {
        if (CurrentChargeCount > 0)
            return true;
        else
            return false;
    }

}
