using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using Cinemachine;

public class CharacterAttackState : CharacterBaseState
{
    private CharacterStateManager _character;
    public enum AttackType
    {
        Light,
        Heavy,
        LongRange,
    }
    private IDamageable _collidedObject;
    private List<Action> _attackActions = new List<Action>();
    
   

    public void AddActionTypes(CharacterStateManager character)
    {
        _character = character;
        _attackActions.Add(NormalAttack);
        _attackActions.Add(HeavyAttack);
        _attackActions.Add(LongRangeAttack);
    }

    public override void EnterState(CharacterStateManager character)
    {
       
         AttackRange(character);
        _attackActions[(int)character.AttackType]();
     
       //character.transform.LookAt(MousePointer.GetWorldRay(Camera.main, false));
        character.StartCoroutine(DelayState(character));
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
               _collidedObject = inSightRange[i].transform.GetComponent<IDamageable>();
               
            }
        }
    }


    private void NormalAttack()
    {
       if (_collidedObject == null) return;
        Debug.Log("Normal Attack");
        _collidedObject.TakeDamage(_character.CharacterProperties.LightAttackDamage);
      


    }

    private void HeavyAttack()
    {
        if (_collidedObject == null) return;
        Debug.Log("Heavy Attack");
        _collidedObject.TakeDamage(_character.CharacterProperties.HeavyAttackDamage);
      
    }

    private void LongRangeAttack()
    {

    }

    public override IEnumerator DelayState(CharacterStateManager character)
    {
        yield return new WaitForSeconds(0.4f);
        ExitState(character);
    }

}
