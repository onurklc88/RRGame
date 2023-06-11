using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class IdleState : IState
{
    public void SetupState(Creature creature)
    {
        Debug.Log("IdleState");
        creature.NavMeshAgent.speed = 0;
        creature.StartCoroutine(DelayState(creature));
       
    }
    public void ProcessState(Creature creature)
    {
        Debug.Log("IDLE PROCESS STATE");
    }

   private IEnumerator DelayState(Creature creature)
   {
        Debug.Log("IDLE DELAY");
        yield return new WaitForSeconds(creature.AnimationDelayTime);
        creature.SwitchState(creature.StateFactory.Walk());
   }
}
