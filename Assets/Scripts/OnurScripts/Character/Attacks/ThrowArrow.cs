using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThrowArrow : CharacterAttackState
{
    float asd = 10f;
    public override void EnterState(CharacterStateManager character)
    {
     
    }
    public override void UpdateState(CharacterStateManager character)
    {
        TrackCursorPosition(character);
    }
    public override void AttackBehaviour(CharacterStateManager character)
    {
       GameObject arrow = ObjectPool.GetPooledObject(0);
       arrow.transform.position = character.transform.position + character.transform.forward;
       
       arrow.SetActive(true);
        Vector3 arrowMovePosition = arrow.transform.position + character.transform.forward * 40f;
        // Vector3 direction = GetMousePosition();

        // direction.y = character.transform.position.y;
        //Vector3 arrowMovePosition = arrow.transform.position + MouseTarget.Target().transform.forward * 10f;
        arrow.transform.DOMove(arrowMovePosition, 1f);
       
       ExitState(character);
    }

    public override void ExitState(CharacterStateManager character)
    {
        character.CharacterStateFactory.CurrentCombatType = CharacterStateFactory.CombatType.None;
        character.SwitchState(character.CharacterStateFactory.CharacterIdleState);
    }
}
