using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMeleeAttack : IState
{
    private bool _alreadyAttacked = false;
    public void SetupState(Creature creature)
    {
        creature.NavMeshAgent.isStopped = true;
        creature.CreatureAnimationController.PlayCreatureAnimation("Attack", true);
        PlayerDetection(creature);
    }
    public void ProcessState(Creature creature)
    {
        PlayerDetection(creature);
    }
    private void PlayerDetection(Creature creature)
    {
        Collider[] inSightRange = Physics.OverlapSphere(creature.transform.position + creature.transform.forward * 1f, 5f, creature.PlayerMask);

        if (inSightRange.Length <= 0 || _alreadyAttacked) return;

        _alreadyAttacked = true;

        if (inSightRange[0].transform.GetComponent<IHealable>() != null)
        {
           
            creature.CharacterStateFactory.KnockBackPosition = creature.transform.forward * 15f;
            inSightRange[0].transform.GetComponent<IDamageable>().TakeDamage(creature.EnemyProperties.LightAttackDamage);
        }
        creature.StartCoroutine(DelayState(creature));
    }

    private IEnumerator DelayState(Creature creature)
    {
        yield return new WaitForSeconds(0.1f);
        if(creature.CurrentCreatureState != creature.EnemyStateFactory.Death)
        {
            creature.CreatureAnimationController.PlayCreatureAnimation("Attack", false);
            creature.NavMeshAgent.isStopped = false;
            _alreadyAttacked = false;
            creature.SwitchState(creature.EnemyStateFactory.Chase);
        }
    }

}
