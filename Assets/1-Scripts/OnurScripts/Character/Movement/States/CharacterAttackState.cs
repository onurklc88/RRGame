using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
public class CharacterAttackState : CharacterBaseState
{
    protected IDamageable _collidedObject;
    private float _attackDashDuration = 0.2f;
    Vector3 _spawnposition;
    Vector3 hitPosition;
    protected Vector3 _hitDirection;
    protected float _totalDamage;
    #region StateProperties
    public override void EnterState(CharacterStateManager character)
    {
        _spawnposition = character.transform.position + character.transform.forward;
    }
  
    public override void ExitState(CharacterStateManager character)
    {
        character.CharacterStateFactory.CharacterAttackState = null;
        character.SwitchState(character.CharacterStateFactory.CharacterIdleState);
    }
    public virtual void DoAttackBehaviour(CharacterStateManager character) { }
    #endregion
    
  
    protected void CheckAttackRange(CharacterStateManager character)
    {
        Collider[] inSightRange = Physics.OverlapSphere(character.transform.position + character.transform.forward * SaveInfo.Player.SelectedWeapon.Range, character.CharacterProperties.AttackArea);
      
        if (inSightRange.Length <= 0) return;
     
        for (int i = 0; i < inSightRange.Length; i++)
        {
            if(inSightRange[i].transform.GetComponent<IDamageable>() != null && inSightRange[i].transform.GetComponent<IHealable>() == null)
            {
               _collidedObject = inSightRange[i].transform.GetComponent<IDamageable>();
            }
        }
    }
    protected void AttackDash(CharacterStateManager character)
    {
        Vector3 dashPoisiton = character.transform.position + character.transform.forward * 15f;
        character.StartCoroutine(MovePlayerToDashPoisiton(character, dashPoisiton));
    }

    protected void TrackCursorPosition(CharacterStateManager character)
    {
        var direction = character.MouseTarget.MousePosition - character.transform.position;
        direction.y = 0f;
        character.transform.forward = direction;
    }
    public override IEnumerator DelayState(CharacterStateManager character)
    {
        yield return new WaitForSeconds(_attackDashDuration);
        ExitState(character);
    }

    private IEnumerator MovePlayerToDashPoisiton(CharacterStateManager character, Vector3 dashPoint)
    {
        float startTime = Time.time;
        while (Time.time < startTime + 0.25)
        {
            dashPoint.y += -50f;
            character.CharacterController.Move((dashPoint - character.transform.position) * Time.deltaTime);
            CheckAttackRange(character);
            yield return null;
        }
        
    }

}
