using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using DG.Tweening;


public class CharacterAttackState : CharacterBaseState
{
    private CharacterStateManager _character;
    public enum CombatType
    {
       Melee,
       LongRange
    }
    public CombatType CurrentCombatType;
    public CharacterAttackState CurrentAttackType;
   

    private IDamageable _collidedObject;
    private float _attackDashDuration = 0.5f;
    public void AddActionTypes(CharacterStateManager character)
    {
     
    }
    
   
    public override void EnterState(CharacterStateManager character)
    {
        _character = character;
    }
    public override void UpdateState(CharacterStateManager character)
    {
        
     
            ChangeCharacterRotation(character);
    }
    public override void ExitState(CharacterStateManager character)
    {
         character.SwitchState(character.CharacterStateFactory.CharacterIdleState);
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


   

    private void MeleeLightAttack()
    {
        Debug.Log("Light Attack ");
        
        Debug.Log("Normal Attack");
        AttackRange(_character);
        ChangeCharacterRotation(_character);
        if (_collidedObject != null) { _collidedObject.TakeDamage(_character.CharacterProperties.LightAttackDamage); }
        AttackDash();
        _character.StartCoroutine(DelayState(_character));
        
    }

    private void MeleeHeavyAttack()
    {
        Debug.Log("Heavy Attack");
        AttackRange(_character);
        ChangeCharacterRotation(_character);
        if (_collidedObject != null) { _collidedObject.TakeDamage(_character.CharacterProperties.HeavyAttackDamage); }
        AttackDash();
        _character.StartCoroutine(DelayState(_character));
    }



   

    private void AttackDash()
    {
        Vector3 _attackPosition = _character.transform.position + _character.transform.forward * 2f;
        _character.transform.DOMove(_attackPosition, _attackDashDuration);
        
    }

    private void ThrowArrow()
    {
        Debug.Log("ThrowArrow");
        _character.StartCoroutine(DelayState(_character));
    }
    


    private void ChangeCharacterRotation(CharacterStateManager character)
    {
        var direction = GetMousePosition() - character.transform.position;
        direction.y = 0;
        character.transform.forward = direction;
    }

    private Vector3 GetMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hitInfo, Mathf.Infinity)) return Vector3.zero;
        return hitInfo.point;
    }

    public override IEnumerator DelayState(CharacterStateManager character)
    {
        yield return new WaitForSeconds(_attackDashDuration);
        ExitState(character);
    }

}
