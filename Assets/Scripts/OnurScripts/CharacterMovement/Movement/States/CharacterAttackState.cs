using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using DG.Tweening;


public class CharacterAttackState : CharacterBaseState
{
    protected IDamageable CollidedObject;
    private float _attackDashDuration = 0.2f;
  
   
    public override void EnterState(CharacterStateManager character)
    {
        
    }
    public override void UpdateState(CharacterStateManager character)
    {
    
    }
    public override void ExitState(CharacterStateManager character)
    {
        character.CharacterStateFactory.CurrentCombatType = CharacterStateFactory.CombatType.None;
        character.SwitchState(character.CharacterStateFactory.CharacterIdleState);
    }
    public virtual void AttackBehaviour(CharacterStateManager character) { }
    protected void AttackRange(CharacterStateManager character)
    {
        Collider[] inSightRange = Physics.OverlapSphere(character.transform.position + character.transform.forward * 2f, character.CharacterProperties.AttackArea);
      
        if (inSightRange.Length <= 0) return;
       
        for (int i = 0; i < inSightRange.Length; i++)
        {
            if(inSightRange[i].transform.GetComponent<IDamageable>() != null)
            {
               CollidedObject = inSightRange[i].transform.GetComponent<IDamageable>();
            }
        }
    }


    protected void AttackDash(CharacterStateManager character)
    {
        Vector3 _attackPosition = character.transform.position + character.transform.forward * 2f;
        character.transform.DOMove(_attackPosition, _attackDashDuration);
    }

    protected void TrackCursorPosition(CharacterStateManager character)
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
