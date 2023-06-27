using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;


public class CharacterAttackState : CharacterBaseState
{
    private CharacterStateManager _character;
    public enum AttackType
    {
        Light,
        Heavy,
        LongRange,
        None
    }
    private IDamageable _collidedObject;
    private List<Action> _attackActions = new List<Action>();
    private float _attackDashDuration = 0.5f;
   

    public void AddActionTypes(CharacterStateManager character)
    {
        _character = character;
        _attackActions.Add(NormalAttack);
        _attackActions.Add(HeavyAttack);
        _attackActions.Add(LongRangeAttack);
    }

    public override void EnterState(CharacterStateManager character)
    {
        if(character.AttackType != AttackType.None)
            _attackActions[(int)character.AttackType]();
    
    }
    public override void UpdateState(CharacterStateManager character)
    {
        
        if (character.LongRangeStarted)
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


    private void NormalAttack()
    {
        Debug.Log("Normal Attack");
        AttackRange(_character);
        ChangeCharacterRotation(_character);
        if (_collidedObject != null) { _collidedObject.TakeDamage(_character.CharacterProperties.LightAttackDamage); }
        AttackDash();
        _character.StartCoroutine(DelayState(_character));
    }

    private void HeavyAttack()
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

    private void LongRangeAttack()
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
