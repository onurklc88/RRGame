using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterSlideState : CharacterBaseState
{


    public override void EnterState(CharacterStateManager character)
    {
        Debug.Log("Slide State:" + character.transform.forward);
        character.transform.DOMove(character.transform.position + character.transform.forward * 7f, 0.5f).SetEase(Ease.Linear).OnComplete(() => ExitState(character)); ;
    }
    public override void UpdateState(CharacterStateManager character)
    {
        
    }
    public override void ExitState(CharacterStateManager character)
    {
        character.IsSlidePressed = false;
        
        character.SwitchState(character.CharacterIdleState);
    }
}
