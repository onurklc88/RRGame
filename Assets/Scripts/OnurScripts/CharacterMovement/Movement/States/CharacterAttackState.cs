using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using DG.Tweening;


public class CharacterAttackState : CharacterBaseState
{
    protected IDamageable _collidedObject;
    private float _attackDashDuration = 0.2f;
    Vector3 _spawnposition;
    Vector3 hitPosition;
    protected Vector3 _hitDirection;


    public override void EnterState(CharacterStateManager character)
    {
        _spawnposition = character.transform.position + character.transform.forward;
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
        Collider[] inSightRange = Physics.OverlapSphere(character.transform.position + character.transform.forward * SaveInfo.Player.SelectedWeapon.Range, character.CharacterProperties.AttackArea);
      
        if (inSightRange.Length <= 0) return;
       
        for (int i = 0; i < inSightRange.Length; i++)
        {
            if(inSightRange[i].transform.GetComponent<IDamageable>() != null)
            {
               _collidedObject = inSightRange[i].transform.GetComponent<IDamageable>();
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
        var direction = MouseTarget.GetMousePosition() - character.transform.position;
        direction.y = 0f;
        character.transform.forward = direction;
        //character.Test.transform.position = GetMousePosition();
    }

  

    public override IEnumerator DelayState(CharacterStateManager character)
    {
        yield return new WaitForSeconds(_attackDashDuration);
        ExitState(character);
    }

    

}
