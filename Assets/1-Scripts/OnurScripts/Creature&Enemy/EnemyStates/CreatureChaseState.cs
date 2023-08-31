using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureChaseState : IState
{

    public void SetupState(Creature creature)
    {
        Debug.Log("Chase State");
        creature.CreatureAnimationController.PlayBlendAnimations(true);
        creature.NavMeshAgent.speed = 2;
    }
    public void ProcessState(Creature creature)
    {
        CheckDistanceBetweenPlayer(creature);
        ChasePlayer(creature);
    }

    private void ChasePlayer(Creature creature)
    {
        creature.NavMeshAgent.SetDestination(creature.PlayerCharacter.transform.position);
    }
    private void CheckDistanceBetweenPlayer(Creature creature)
    {
        if (creature.PlayerCharacter == null) return;

        float distanceBetweenPlayer = Vector3.Distance(creature.PlayerCharacter.transform.position, creature.transform.position);

        if (distanceBetweenPlayer < creature.EnemyProperties.AttackDistance)
        {
            creature.CreatureAnimationController.PlayBlendAnimations(false);
            creature.SwitchState(creature.CreatureAttackType);
        }
            
    }
}

