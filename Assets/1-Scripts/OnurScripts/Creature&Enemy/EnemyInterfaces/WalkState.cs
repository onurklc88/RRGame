using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkState : IState
{
    
    public void SetupState(Creature creature)
    {
        creature.NavMeshAgent.speed = 2;
        //Debug.Log("CurrentWaypointIndex: " + creature._currentWaypointIndex); 
    }

    public void ProcessState(Creature creature)
    {
        Patrol(creature);
    }

    
    private void Patrol(Creature creature)
    {


        if (creature.transform.position.x != creature.Waypoint[creature.CurrentWaypointIndex].position.x)
        {
            creature.NavMeshAgent.SetDestination(creature.Waypoint[creature.CurrentWaypointIndex].position);
        }
        else
        {
         
            creature.SwitchState(creature.StateFactory.Idle());
            creature.CurrentWaypointIndex = (creature.CurrentWaypointIndex + 1) % creature.Waypoint.Length;
           
        }

       
    }
   
}
