using UnityEngine;
using DG.Tweening;

public class ThrowBomb : CharacterAttackState
{
   
    public override void UpdateState(CharacterStateManager character)
    {
        TrackCursorPosition(character);
    }
    public override void DoAttackBehaviour(CharacterStateManager character)
    {
        if (!character.WeaponHandler.IsChargeReady()) { ExitState(character); return; }
        GameObject bomb = ObjectPool.GetPooledObject(1);
        bomb.transform.position = character.transform.position + character.transform.forward;
        bomb.SetActive(true);
        Vector3 bombMovePosition = new Vector3(bomb.transform.position.x, -1f, bomb.transform.position.z) + character.transform.forward * 15f;
        bomb.transform.DOJump(bombMovePosition, 10f, 1, 0.5f, false).SetEase(Ease.Linear);
        EventLibrary.OnWeaponChargeUpdated.Invoke(false);
        ExitState(character);
    }

    public override void ExitState(CharacterStateManager character)
    {
       character.SwitchState(character.CharacterStateFactory.CharacterIdleState);
    }
}
