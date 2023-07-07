using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttack : CharacterAttackState
{

    public override void EnterState(CharacterStateManager character)
    {
        AttackBehaviour(character);
    }

    public override void AttackBehaviour(CharacterStateManager character)
    {
        Debug.Log("Greetings from Light Attack");
        AttackRange(character);
        TrackCursorPosition(character);
        if (CollidedObject != null) { CollidedObject.TakeDamage(character.CharacterProperties.LightAttackDamage); }
        AttackDash(character);
        character.StartCoroutine(DelayState(character));
    }

    
}
