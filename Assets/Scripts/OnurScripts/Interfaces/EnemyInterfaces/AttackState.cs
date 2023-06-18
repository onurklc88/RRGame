using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttackState : IState
{
    private Tween _dash;
    private Vector3 _dashPoint;
    private bool _alreadyAttacked = false;
   
    public void SetupState(Creature creature)
    {
        creature.NavMeshAgent.isStopped = true;
        creature.transform.LookAt(creature.PlayerCharacter.transform.position);
        _dashPoint = creature.transform.position + (creature.transform.forward * 7f);
        PlayAttackTween(creature);
       
    }
    public void ProcessState(Creature creature)
    {
       
    }

    private void PlayAttackTween(Creature creature)
    {
        _dash = creature.transform.DOMove(_dashPoint, creature.EnemyProperties.AttackTime);
        _dash.OnUpdate(() => 
        {
            PlayerDetection(creature);
        });

        _dash.OnComplete(() =>
        {
            creature.StartCoroutine(DelayState(creature));
        });

    }

    private void PlayerDetection(Creature creature)
    {
        Collider[] inSightRange = Physics.OverlapSphere(creature.transform.position + creature.transform.forward * 1f, 1f, creature.PlayerMask);
       
        if (inSightRange.Length <= 0 || _alreadyAttacked) return;

        _alreadyAttacked = true;

        if (inSightRange[0].transform.GetComponent<IHealable>() != null)
              inSightRange[0].transform.GetComponent<IDamageable>().TakeDamage(creature.EnemyProperties.LightAttackDamage);
    }

    private IEnumerator DelayState(Creature creature)
    {
        
        yield return new WaitForSeconds(1f);
        creature.NavMeshAgent.isStopped = false;
        _alreadyAttacked = false;
        creature.SwitchState(creature.StateFactory.Chase());
    }


}
