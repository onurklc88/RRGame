using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class WeaponHandler : MonoBehaviour
{
    
    private PlayerInput _playerInput;
    private Arrow _arrow = new Arrow();
    private Bomb _bomb = new Bomb();
   // private CharacterStateManager ch = new CharacterStateManager();
    private Dictionary<string, Weapon> _weaponDictionary = new Dictionary<string, Weapon>();

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
        EventLibrary.OnWeaponChange.Invoke(_arrow);
        _weaponDictionary.Add("Arrow", _arrow);
        _weaponDictionary.Add("Bomb", _bomb);
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
