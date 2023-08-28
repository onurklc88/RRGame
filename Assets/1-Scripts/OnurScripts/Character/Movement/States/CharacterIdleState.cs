using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterIdleState : CharacterBaseState
{
    
    public override void EnterState(CharacterStateManager character) 
    {
       //Debug.Log("Welcome to IdleState");

    } 
    public override void UpdateState(CharacterStateManager character)
    {
        //eðer attack state'ten buraya gelirsek delay koy
        if (character.IsMovementPressed)
             ExitState(character);
    }
    public override void ExitState(CharacterStateManager character)
    {
        character.SwitchState(character.CharacterStateFactory.CharacterWalkState);
    }
}
