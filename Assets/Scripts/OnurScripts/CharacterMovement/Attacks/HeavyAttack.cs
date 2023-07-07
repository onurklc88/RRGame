using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttack : CharacterAttackState
{
    public override void EnterState(CharacterStateManager character)
    {
        AttackBehaviour(character);
    }
    public override void AttackBehaviour(CharacterStateManager character)
    {
        Debug.Log("Greetings from Heavy Attack");
        AttackRange(character);
        TrackCursorPosition(character);
        if (CollidedObject != null) { CollidedObject.TakeDamage(character.CharacterProperties.HeavyAttackDamage); }
        AttackDash(character);
        character.StartCoroutine(DelayState(character));
    }
}
