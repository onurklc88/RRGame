using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterWalkState : CharacterBaseState
{
    //variables to store player input values
    

    public override void EnterState(CharacterStateManager character)
    {
        EventLibrary.StartRunAnimation.Invoke(true);
    }
    public override void UpdateState(CharacterStateManager character)
    {
        if (character.IsMovementPressed)
            character.CharacterController.Move(character.CurrentMove * Time.deltaTime * character.CharacterSpeed);
       else
            ExitState(character);
    }
    public override void ExitState(CharacterStateManager character)
    {
        EventLibrary.StartRunAnimation.Invoke(false);
        character.SwitchState(character.CharacterStateFactory.CharacterIdleState);
    }

 
}