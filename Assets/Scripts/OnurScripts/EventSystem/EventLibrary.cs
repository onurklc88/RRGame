using Cinemachine;
using System;

public static class EventLibrary
{
    //CharacterEvents
    public static readonly GameEvent<CharacterBaseState> OnPlayerTakeDamage = new GameEvent<CharacterBaseState>();
    
    
    //Weapon events
    public static readonly GameEvent<ThrowableWeapon> OnWeaponChange = new GameEvent<ThrowableWeapon>();

   
    
    //Camera events
    public static readonly GameEvent<bool> OnLongRangeAttack = new GameEvent<bool>();
    public static readonly GameEvent<CinemachineImpulseSource> OnWeaponDestroy = new GameEvent<CinemachineImpulseSource>();

    //AnimationEvents
    public static readonly GameEvent<bool> StartRunAnimation = new GameEvent<bool>();
    public static readonly GameEvent<bool> PlayDashAnimation = new GameEvent<bool>();


    
}
