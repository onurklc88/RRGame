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
        CheckAttackRange(character);
        TrackCursorPosition(character);
        if (_collidedObject != null) 
        {
            EventLibrary.OnWeaponChargeUpdated.Invoke(true);
            _collidedObject.TakeDamage(character.CharacterProperties.HeavyAttackDamage); 
        }
        AttackDash(character);
        character.StartCoroutine(DelayState(character));
    }
}
