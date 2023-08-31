using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreatureWalkState : IState
{
    
    public void SetupState(Creature creature)
    {
       
        creature.NavMeshAgent.speed = 2;
        creature.CreatureAnimationController.PlayBlendAnimations(true);
        //creature.NavMeshAgent.SetDestination(creature.Waypoints[creature.CurrentWaypointIndex]);
    }

    public void ProcessState(Creature creature)
    {
        Patrol(creature);
    }

    
    private void Patrol(Creature creature)
    {

        
        if (((int)creature.transform.position.x) != ((int)creature.Waypoints[creature.CurrentWaypointIndex].x))
        {
           creature.NavMeshAgent.SetDestination(creature.Waypoints[creature.CurrentWaypointIndex]);
        }
        else
        {
            creature.CreatureAnimationController.PlayBlendAnimations(false);
            creature.SwitchState(creature.EnemyStateFactory.Idle());
            creature.CurrentWaypointIndex = (creature.CurrentWaypointIndex + 1) % creature.Waypoints.Count;
           
        }

       
    }
   
}
