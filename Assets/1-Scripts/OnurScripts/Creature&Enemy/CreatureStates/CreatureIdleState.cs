using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class CreatureIdleState : IState
{
    public void SetupState(Creature creature)
    {
        creature.NavMeshAgent.speed = 0;
        creature.CreatureAnimationController.PlayCreatureAnimation("Idle", true);
        creature.StartCoroutine(DelayState(creature));
    }

    public void ProcessState(Creature creature)
    {
        PlayerDetection(creature);
    }
   
    public void PlayerDetection(Creature creature)
    {
        Collider[] inSightRange = Physics.OverlapSphere(creature.transform.position, creature.EnemyProperties.AgressionRange, creature.PlayerMask);

        if (inSightRange.Length == 0) return;
        creature.PlayerCharacter = inSightRange[0].gameObject;
        creature.SwitchState(creature.EnemyStateFactory.Chase);
    }

    private IEnumerator DelayState(Creature creature)
    {
       
        yield return new WaitForSeconds(creature.AnimationDelayTime);
        
        if (creature.CurrentCreatureState != creature.EnemyStateFactory.Chase)
        {
            creature.CreatureAnimationController.PlayCreatureAnimation("Idle", false);
            creature.SwitchState(creature.EnemyStateFactory.Walk());
        }
      
    }
}
