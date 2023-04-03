using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterSlideState : CharacterBaseState
{

    Vector3 velocity;
    public override void EnterState(CharacterStateManager character)
    {
       character.StartCoroutine(DelayState(character));
    }
    public override void UpdateState(CharacterStateManager character)
    {
       
    }
    public override void ExitState(CharacterStateManager character)
    {
        character.IsSlidePressed = false;
        
        character.SwitchState(character.CharacterIdleState);
    }

    public override IEnumerator DelayState(CharacterStateManager character)
    {
        float startTime = Time.time;
        while(Time.time < startTime + 0.25)
        {
            
            character.CharacterController.Move(character.transform.forward * 15f * Time.deltaTime);
          
            yield return null;
        }

       
        ExitState(character);
    }
}
