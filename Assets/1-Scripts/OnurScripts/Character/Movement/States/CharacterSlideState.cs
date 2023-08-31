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
      
        EventLibrary.PlayDashAnimation.Invoke(true);
        EventLibrary.OnCharacterDash.Invoke();
        _slideRotation.x = 0f;
        _slideRotation.y = 0f;
        _slideRotation.z = character.CurrentMove.z;
        
      
        Quaternion targetRotation = Quaternion.LookRotation(_slideRotation);
        character.transform.rotation = Quaternion.Slerp(character.transform.rotation, targetRotation, 1f * Time.deltaTime);
        if (character.IsMovementPressed)
            _dashPosition = character.transform.position + character.CurrentMove * 70f;
        else
           _dashPosition = character.transform.position + character.transform.forward * 70f;
        
        character.StartCoroutine(DelayState(character));
    }
    public override void UpdateState(CharacterStateManager character)
    {
       
    }
    public override void ExitState(CharacterStateManager character)
    {
        character.IsSlidePressed = false;
        EventLibrary.PlayDashAnimation.Invoke(false);
        character.SwitchState(character.CharacterStateFactory.CharacterIdleState);
    }

    public override IEnumerator DelayState(CharacterStateManager character)
    {
        float startTime = Time.time;
        while(Time.time < startTime + 0.25f)
        {
            _dashPosition.y += -50f;
            character.CharacterController.Move((_dashPosition - character.transform.position) * Time.deltaTime * 1.5f);
            yield return null;
        }

        //yield return new WaitForSeconds(1f);
        ExitState(character);
    }


}
