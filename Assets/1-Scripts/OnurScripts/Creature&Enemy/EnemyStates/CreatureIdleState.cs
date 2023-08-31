using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class CreatureIdleState : IState
{
    public void SetupState(Creature creature)
    {
       // Debug.Log("IdleState");
        creature.NavMeshAgent.speed = 0;
        creature.CreatureAnimationController.PlayCreatureAnimation("Idle", true);
        creature.StartCoroutine(DelayState(creature));
    }
    

   private IEnumerator DelayState(Creature creature)
   {
        yield return new WaitForSeconds(creature.AnimationDelayTime);
        creature.CreatureAnimationController.PlayCreatureAnimation("Idle", false);
        creature.SwitchState(creature.EnemyStateFactory.Walk());
   }
}
