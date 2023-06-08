using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : IState
{
   public void ProcessState(Creature creature)
   {
        creature.NavMeshAgent.speed = 0;
        //animation
   }
}
