using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttack : CharacterAttackState
{
    public override void EnterState(CharacterStateManager character)
    {
        Debug.Log("LightAttack");
    }
}
