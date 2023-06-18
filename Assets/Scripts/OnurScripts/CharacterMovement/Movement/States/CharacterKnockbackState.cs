using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterKnockbackState : CharacterBaseState
{
  

    public override void EnterState(CharacterStateManager character)
    {
        Debug.Log("Knockback State");
        character.StartCoroutine(DelayState(character));
    }
    public override void UpdateState(CharacterStateManager character)
    {

      
    }
    public override void ExitState(CharacterStateManager character)
    {
        
    }

    public override IEnumerator DelayState(CharacterStateManager character)
    {
        Vector3 knockbackPosition = character.transform.position + -character.transform.forward * 1f;
        float startTime = Time.time;
        while (Time.time < startTime + 0.25)
        {
           
            character.CharacterController.Move((knockbackPosition) * Time.deltaTime);


            yield return null;
        }


        ExitState(character);
    }
}

