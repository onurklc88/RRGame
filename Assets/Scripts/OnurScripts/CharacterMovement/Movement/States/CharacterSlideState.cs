using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterSlideState : CharacterBaseState
{

    private Vector3 _dashPosition;
    private Vector3 _slideRotation;
    public override void EnterState(CharacterStateManager character)
    {
        // if (character.IsSlidePressed) return;
        _slideRotation.x = 0f;
        _slideRotation.y = 0f;
        _slideRotation.z = character.CurrentMove.z;
        Quaternion targetRotation = Quaternion.LookRotation(_slideRotation);
        character.transform.rotation = Quaternion.Slerp(character.transform.rotation, targetRotation, 1f * Time.deltaTime);
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
            
            if (character.transform.position.y > 1.2f)
             _dashPosition = character.transform.position + character.CurrentMove * 15f;
            else
              _dashPosition = character.transform.position + character.transform.forward * 15f;
            
                
              
           character.CharacterController.Move((_dashPosition - character.transform.position) * Time.deltaTime);
           yield return null;
        }

       
        ExitState(character);
    }
}
