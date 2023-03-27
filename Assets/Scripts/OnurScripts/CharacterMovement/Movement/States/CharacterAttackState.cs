using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackState : CharacterBaseState
{
    public enum AttackType
    {
        None,
        Normal,
        Heavy,
    }

    public override void EnterState(CharacterStateManager character)
    {
        Debug.Log("Character Attack State");
        AttackRange(character);
    }
    public override void UpdateState(CharacterStateManager character)
    {
        //AttackRange(character);
    }
    public override void ExitState(CharacterStateManager character)
    {
        character.SwitchState(character.CharacterIdleState);
        
    }

    private void AttackRange(CharacterStateManager character)
    {
        Collider[] inSightRange = Physics.OverlapSphere(character.transform.position + character.transform.forward * 2f, character.CharacterProperties.AttackArea);
      
        if (inSightRange.Length <= 0) return;
       
        for (int i = 0; i < inSightRange.Length; i++)
        {
            if(inSightRange[i].transform.GetComponent<IDamageable>() != null)
            {
               IDamageable collidedObject = inSightRange[i].transform.GetComponent<IDamageable>();
               
            }
        }
    }


    private void NormalAttack(IDamageable collidedObject)
    {

    }

    private void HeavyAttack()
    {

    }

    private void LongRangeAttack()
    {

    }
}
