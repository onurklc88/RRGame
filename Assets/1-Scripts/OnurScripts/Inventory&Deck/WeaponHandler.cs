using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class WeaponHandler : MonoBehaviour
{
    
    private PlayerInput _playerInput;
    private Weapons _weapons = new Weapons();
    
    private Dictionary<string, ThrowableWeapon> _weaponDictionary = new Dictionary<string, ThrowableWeapon>();

    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }
    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }

    private void Start()
    {
        EventLibrary.OnWeaponChange.Invoke(_weapons.Arrow);
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
      EventLibrary.OnWeaponChange.Invoke(_weaponDictionary[context.action.name]);
    }

}
