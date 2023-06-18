using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterSlideState : CharacterBaseState
{

    private Vector3 _dashPosition;
    public override void EnterState(CharacterStateManager character)
    {
       // if (character.IsSlidePressed) return;
        character.StartCoroutine(DelayState(character));
    }
    public override void UpdateState(CharacterStateManager character)
    {
       
    }
    public override void ExitState(CharacterStateManager character)
    {
        character.IsSlidePressed = false;
        character.SwitchState(character.CharacterStateFactory.CharacterIdleState);
    }

    public override IEnumerator DelayState(CharacterStateManager character)
    {
        float startTime = Time.time;
        while(Time.time < startTime + 0.25)
        {
            
            if (character.CurrentMove.magnitude < 1f)
                _dashPosition = character.transform.position + character.transform.forward * 15f;
            else
                _dashPosition = character.transform.position + character.CurrentMove * 15f;
              
           
         
            character.CharacterController.Move((_dashPosition - character.transform.position) * Time.deltaTime);


            yield return null;
        }

       
        ExitState(character);
    }
}
