using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClimbState : CharacterBaseState
{
    public override void EnterState(CharacterStateManager character)
    {
        Debug.Log("Greetings from ClimbState");
        character.StartCoroutine(DelayState(character));
        
    }
    public override void UpdateState(CharacterStateManager character)
    {

    }
    public override void ExitState(CharacterStateManager character)
    {
        Debug.Log("Quit climb state");
        character.SwitchState(character.CharacterStateFactory.CharacterIdleState);

    }

    public override IEnumerator DelayState(CharacterStateManager character)
    {
        yield return new WaitForSeconds(1f);
        ExitState(character);
    }


}
