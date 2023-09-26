using UnityEngine;
using DG.Tweening;

public class ThrowBomb : CharacterAttackState
{
    private float _minJumpPower = 15f; 
    private float _maxJumpPower = 20f; 

    public override void EnterState(CharacterStateManager character)
    {
        character.CharacterContainer.ColorBottle.SetActive(true);
        EventLibrary.OnPlayerThrowBomb.Invoke(true);
    }

    public override void UpdateState(CharacterStateManager character)
    {
        TrackCursorPosition(character);
    }
    public override void DoAttackBehaviour(CharacterStateManager character)
    {
        if (!character.WeaponHandler.IsChargeReady()) { ExitState(character); return; }
        GameObject bomb = ObjectPool.GetPooledObject(1);
        //bomb.transform.position = character.transform.position + character.transform.forward;
        bomb.transform.position = character.CharacterContainer.ColorBottle.transform.position;
        character.CharacterContainer.ColorBottle.SetActive(false);
        bomb.SetActive(true);
        Vector3 bombMovePosition = new Vector3(bomb.transform.position.x, character.transform.position.y - 5f, bomb.transform.position.z) + character.transform.forward * 15f;
       
      
        float bombJumpPower = Mathf.Lerp(_minJumpPower, _maxJumpPower, character.transform.position.y / character.transform.position.y + 5f);

        bomb.transform.DOJump(bombMovePosition, bombJumpPower, 1, 0.7f, false).SetEase(Ease.Linear).OnUpdate(() => bomb.transform.DORotate(new Vector3(180f, 0f, 0f), 0.5f));
        EventLibrary.OnPlayerThrowBomb.Invoke(false);
        EventLibrary.OnWeaponChargeUpdated.Invoke(false);
        ExitState(character);
    }

    public override void ExitState(CharacterStateManager character)
    {
        character.CharacterContainer.ColorBottle.SetActive(false);
        EventLibrary.OnPlayerThrowBomb.Invoke(false);
        character.SwitchState(character.CharacterStateFactory.CharacterIdleState);
    }
}
