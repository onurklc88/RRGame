using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThrowArrow : CharacterAttackState
{
    public override void UpdateState(CharacterStateManager character)
    {
        TrackCursorPosition(character);
    }
    public override void DoAttackBehaviour(CharacterStateManager character)
    {
        Debug.Log("bool: " + character.WeaponHandler.IsChargeReady());
       if (!character.WeaponHandler.IsChargeReady()) { ExitState(character); return; }
       
       GameObject arrow = ObjectPool.GetPooledObject(0);
       arrow.transform.position = character.transform.position + character.transform.forward;
       arrow.SetActive(true);
       Vector3 arrowMovePosition = arrow.transform.position + character.transform.forward * 80f;
       arrow.transform.DOMove(arrowMovePosition, 1f).OnComplete(() => EventLibrary.ResetPooledObject.Invoke(arrow));
       EventLibrary.OnWeaponChargeUpdated.Invoke(false);
       ExitState(character);
    }

    public override void ExitState(CharacterStateManager character)
    {
        character.SwitchState(character.CharacterStateFactory.CharacterIdleState);
    }
}