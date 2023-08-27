using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreatureWalkState : IState
{
    
    public void SetupState(Creature creature)
    {
        creature.NavMeshAgent.speed = 2;
        //creature.NavMeshAgent.SetDestination(creature.Waypoints[creature.CurrentWaypointIndex]);
    }

    public void ProcessState(Creature creature)
    {
        Patrol(creature);
    }

    
    private void Patrol(Creature creature)
    {

        
        if (creature.transform.position.x != creature.Waypoints[creature.CurrentWaypointIndex].x)
        {
            creature.NavMeshAgent.SetDestination(creature.Waypoints[creature.CurrentWaypointIndex]);
        }
        else
        {
         
            creature.SwitchState(creature.EnemyStateFactory.Idle());
            creature.CurrentWaypointIndex = (creature.CurrentWaypointIndex + 1) % creature.Waypoints.Count;
           
        }

       
    }
   
}
