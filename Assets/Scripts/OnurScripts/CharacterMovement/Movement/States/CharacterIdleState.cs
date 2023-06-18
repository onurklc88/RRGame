using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdleState : CharacterBaseState
{
    public override void EnterState(CharacterStateManager character) 
    {
       // Debug.Log("Welcome to IdleState");
    } 
    public override void UpdateState(CharacterStateManager character)
    {
        
        if (character.IsMovementPressed)
             ExitState(character);
    }
    public override void ExitState(CharacterStateManager character)
    {
        character.SwitchState(character.CharacterStateFactory.CharacterWalkState);
    }
}
