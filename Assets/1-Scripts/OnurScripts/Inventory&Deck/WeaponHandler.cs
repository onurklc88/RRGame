using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Zenject;


public class WeaponHandler : MonoBehaviour
{
    
    private PlayerInput _playerInput;
    [Inject]
    Weapons _weapons;
    private ThrowableWeapon _currentWeapon;
    private int _totalCharge = 0;
    private int _currentChargeCount = 0;
    private Dictionary<string, ThrowableWeapon> _weaponDictionary = new Dictionary<string, ThrowableWeapon>();

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
    private void OnWeaponChargeLoaded(bool isCharged)
    {
        if (isCharged && _currentChargeCount < _totalCharge)
        {
            _currentChargeCount++;
        }
        else if(_currentChargeCount >= 0)
        {
            _currentChargeCount--;
        }
    }

    public ThrowableWeapon HandedWeapon()
    {
        return _currentWeapon;
    }

    public bool IsChargeReady()
    {
        if (_currentChargeCount > 0)
            return true;
        else
            return false;
    }

}
