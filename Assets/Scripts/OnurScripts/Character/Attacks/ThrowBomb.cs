using UnityEngine;
using DG.Tweening;

public class ThrowBomb : CharacterAttackState
{
    public override void EnterState(CharacterStateManager character)
    {
      
    }
    public override void UpdateState(CharacterStateManager character)
    {
        TrackCursorPosition(character);
    }
    public override void AttackBehaviour(CharacterStateManager character)
    {
        GameObject bomb = ObjectPool.GetPooledObject(1);
        bomb.transform.position = character.transform.position + character.transform.forward;
        bomb.SetActive(true);
        Vector3 bombMovePosition = new Vector3(bomb.transform.position.x, 0f, bomb.transform.position.z) + character.transform.forward * 15f;
        bomb.transform.DOJump(bombMovePosition, 10f, 1, 0.5f, false).SetEase(Ease.Linear);
        ExitState(character);
    }

    public override void ExitState(CharacterStateManager character)
    {
        character.CharacterStateFactory.CurrentCombatType = CharacterStateFactory.CombatType.None;
        character.SwitchState(character.CharacterStateFactory.CharacterIdleState);
    }
}
