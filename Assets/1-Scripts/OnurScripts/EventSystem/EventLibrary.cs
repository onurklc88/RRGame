using System;
using UnityEngine;
public static class EventLibrary
{
    //CharacterEvents
    public static readonly GameEvent<CharacterBaseState> OnPlayerTakeDamage = new GameEvent<CharacterBaseState>();


    //Weapon events
    public static readonly GameEvent<bool> OnWeaponChargeUpdated = new GameEvent<bool>();
    public static readonly GameEvent<ThrowableWeapon> OnWeaponSwitch = new GameEvent<ThrowableWeapon>();
   
    
    //Camera events
    public static readonly GameEvent<bool> OnLongRangeAttack = new GameEvent<bool>();
  

    //AnimationEvents
    public static readonly GameEvent<bool> StartRunAnimation = new GameEvent<bool>();
    public static readonly GameEvent<bool> PlayDashAnimation = new GameEvent<bool>();
    public static readonly GameEvent<bool> PlayAttackAnimation = new GameEvent<bool>();
    public static readonly GameEvent<bool> OnPlayerThrowBomb = new GameEvent<bool>();

    //VFX Events
    public static readonly GameEvent OnCharacterDash = new GameEvent();

    //Object Pool
    public static readonly GameEvent<GameObject> ResetPooledObject = new GameEvent<GameObject>();
    
}
