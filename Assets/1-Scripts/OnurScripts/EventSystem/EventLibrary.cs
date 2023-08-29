using System;
using UnityEngine;
public static class EventLibrary
{
    //CharacterEvents
    public static readonly GameEvent<CharacterBaseState> OnPlayerTakeDamage = new GameEvent<CharacterBaseState>();


    //Weapon events
    public static readonly GameEvent<bool> OnWeaponChargeUpdated = new GameEvent<bool>();
   
    
    //Camera events
    public static readonly GameEvent<bool> OnLongRangeAttack = new GameEvent<bool>();
  

    //AnimationEvents
    public static readonly GameEvent<bool> StartRunAnimation = new GameEvent<bool>();
    public static readonly GameEvent<bool> PlayDashAnimation = new GameEvent<bool>();

    //Object Pool
    public static readonly GameEvent<GameObject> ResetPooledObject = new GameEvent<GameObject>();
    
}
