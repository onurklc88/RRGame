using UnityEngine;
using DG.Tweening;

public class ThrowBomb : CharacterAttackState
{
    private float _distance;
    float maxDistance = 10f;
    private float _normalizedDistance;
    // _distance'ý maksimum uzaklýkla normalleþtirin



    public override void EnterState(CharacterStateManager character)
    {
        character.CharacterContainer.ColorBottle.SetActive(true);
        EventLibrary.OnPlayerThrowBomb.Invoke(true);
    }

    public override void UpdateState(CharacterStateManager character)
    {
        TrackCursorPosition(character);
        _distance = Vector3.Distance(character.transform.position, character.MouseTarget.MousePosition);
        _distance = Mathf.Clamp(_distance, 8f, 30f);
        //float bombJumpPower = Mathf.Lerp(4f, 30f, _distance / 100f) * 10;
        float bombJumpPower = _distance * 2;
        character.TrajectoryDrawer.Draw(character.transform.position,
            (character.transform.forward + character.transform.up * 2.5f).normalized * bombJumpPower);

    }
    public override void DoAttackBehaviour(CharacterStateManager character)
    {
        if (!character.WeaponHandler.IsChargeReady()) { ExitState(character); return; }
        GameObject bomb = ObjectPool.GetPooledObject(1);
        bomb.transform.position = character.CharacterContainer.ColorBottle.transform.position;
        character.CharacterContainer.ColorBottle.SetActive(false);
        bomb.SetActive(true);
        Rigidbody bombRigidbody = bomb.transform.GetComponent<Rigidbody>();
        bombRigidbody.isKinematic = false;
        //float bombJumpPower = Mathf.Lerp(4f, 9f, _distance / 100f) * 10;
        float bombJumpPower = _distance * 2.2f;

        bombRigidbody.AddForce((character.transform.forward + character.transform.up * 2.5f).normalized * bombJumpPower, ForceMode.Impulse);
        
        EventLibrary.OnPlayerThrowBomb.Invoke(false);
        EventLibrary.OnWeaponChargeUpdated.Invoke(false);
        ExitState(character);
    }

    public override void ExitState(CharacterStateManager character)
    {
        _distance = 0;
        character.TrajectoryDrawer.Clear();
        character.CharacterContainer.ColorBottle.SetActive(false);
        EventLibrary.OnPlayerThrowBomb.Invoke(false);
        character.SwitchState(character.CharacterStateFactory.CharacterIdleState);
    }
}
