using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureChaseState : IState
{

    public void SetupState(Creature creature)
    {
        
        if(!creature.CreatureAnimationController.AlreadyWalked)
            creature.CreatureAnimationController.PlayBlendAnimations(true);
     
        creature.GetComponent<Animator>().speed = 2f;
        creature.NavMeshAgent.speed = 3.5f;
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
        if (creature.CurrentCreatureState == creature.EnemyStateFactory.Death) return;
      
        float distanceBetweenPlayer = Vector3.Distance(creature.PlayerCharacter.transform.position, creature.transform.position);
       
        RotateCreature(creature);
        if (distanceBetweenPlayer <= creature.EnemyProperties.AttackDistance)
        {
            creature.GetComponent<Animator>().speed = 1;
            creature.CreatureAnimationController.PlayBlendAnimations(false);
            creature.SwitchState(creature.CreatureAttackType);
        }
            
    }


    private void RotateCreature(Creature creature)
    {
        Vector3 lookDirection = creature.PlayerCharacter.transform.position - creature.transform.position;
        float maxRotationSpeed = 300f; 
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        creature.transform.rotation = Quaternion.RotateTowards(creature.transform.rotation, targetRotation, maxRotationSpeed * Time.deltaTime);
    }
}

