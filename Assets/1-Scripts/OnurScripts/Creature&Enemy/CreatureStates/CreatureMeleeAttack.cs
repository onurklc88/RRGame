using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMeleeAttack : IState
{
    private bool _alreadyAttacked = false;
    private float _timer = 0f;
    public void SetupState(Creature creature)
    {
        creature.NavMeshAgent.isStopped = true;
      
    }
    public void ProcessState(Creature creature)
    {
        CheckDistanceBetweenPlayer(creature);
        AttackTimer(creature);

    }
    private void CheckDistanceBetweenPlayer(Creature creature)
    {
        if (creature.CurrentCreatureState == creature.EnemyStateFactory.Death) return;

        float distanceBetweenPlayer = Vector3.Distance(creature.PlayerCharacter.transform.position, creature.transform.position);
       // creature.transform.LookAt(creature.PlayerCharacter.transform.position);
      
        if (distanceBetweenPlayer > creature.EnemyProperties.AttackDistance)
        {
            _timer = 0;
            ExitState(creature);
        }

    }

    private void AttackTimer(Creature creature)
    {
        _timer += Time.deltaTime;
   
        if (_timer >= creature.EnemyProperties.AttackTime)
        {
            PlayerDetection(creature);
            _timer = 0f;
        }
    }
    private void PlayerDetection(Creature creature)
    {
        Collider[] inSightRange = Physics.OverlapSphere(creature.transform.position + creature.transform.forward * 1f, 5f, creature.PlayerMask);

        if (inSightRange.Length <= 0 || _alreadyAttacked) return;

        _alreadyAttacked = true;

        if (inSightRange[0].transform.GetComponent<IHealable>() != null)
        {
            creature.CharacterStateFactory.KnockBackPosition = creature.transform.forward * 15f;
            creature.CreatureAnimationController.PlayCreatureAnimation("Attack", true);
            inSightRange[0].transform.GetComponent<IDamageable>().TakeDamage(creature.EnemyProperties.LightAttackDamage, creature.EnemyProperties.AttackType);
        }
       // creature.StartCoroutine(DelayState(creature));
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

    private void ExitState(Creature creature)
    {
        creature.CreatureAnimationController.PlayCreatureAnimation("Attack", false);
        creature.NavMeshAgent.isStopped = false;
        _alreadyAttacked = false;
        creature.SwitchState(creature.EnemyStateFactory.Chase);
    }

    private IEnumerator DelayAttack(Creature creature)
    {
       
        yield return new WaitForSeconds(creature.EnemyProperties.AttackTime);
       // creature.GetComponent<Animator>().speed = 1;
        creature.CreatureAnimationController.PlayCreatureAnimation("Attack", true);
        PlayerDetection(creature);
    }

}
