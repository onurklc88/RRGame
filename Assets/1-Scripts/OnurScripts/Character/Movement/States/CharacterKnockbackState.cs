using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterKnockbackState : CharacterBaseState
{
  

    public override void EnterState(CharacterStateManager character)
    {
         character.StartCoroutine(DelayState(character));
       
    }
   
    public override void ExitState(CharacterStateManager character)
    {
        character.SwitchState(character.CharacterStateFactory.CharacterIdleState);
    }

    public override IEnumerator DelayState(CharacterStateManager character)
    {
        //Vector3 knockbackPosition = -character.transform.forward * 30f;
        Debug.Log("Character knockback pos: " +character.CharacterStateFactory.KnockBackPosition);

        float startTime = Time.time;
        while (Time.time < startTime + 0.25)
        {
           character.CharacterController.Move((character.CharacterStateFactory.KnockBackPosition) * Time.deltaTime);
           yield return null;
        }


        ExitState(character);
    }
}

