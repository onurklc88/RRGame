using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttack : CharacterAttackState
{
    public override void EnterState(CharacterStateManager character)
    {
        DoAttackBehaviour(character);
    }
    public override void DoAttackBehaviour(CharacterStateManager character)
    {
        //Debug.Log("Greetings from Heavy Attack");
        _totalDamage = SaveInfo.Player.SelectedWeapon.Damage + SaveInfo.UpgradeSave.StrengthDamage + 0.2f;
        CheckAttackRange(character);
        TrackCursorPosition(character);
        if (_collidedObject != null) 
        {
            EventLibrary.OnWeaponChargeUpdated.Invoke(true);
            _collidedObject.TakeDamage(_totalDamage, SaveInfo.Player.SelectedWeapon.DamageType); 
        }
        AttackDash(character);
        character.StartCoroutine(DelayState(character));
    }
}
