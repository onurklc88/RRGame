using Cinemachine;
using System;

public static class EventLibrary
{
    //CharacterEvents
    public static readonly GameEvent<CharacterBaseState> OnPlayerTakeDamage = new GameEvent<CharacterBaseState>();
    
    
    //Weapon events
    public static readonly GameEvent<Weapon> OnWeaponChange = new GameEvent<Weapon>();
   
    
    //Camera events
    public static readonly GameEvent<bool> OnLongRangeAttack = new GameEvent<bool>();
    public static readonly GameEvent<CinemachineImpulseSource> OnWeaponDestroy = new GameEvent<CinemachineImpulseSource>();


    
}
