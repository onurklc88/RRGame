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
    }

    public void ProcessState(Creature creature)
    {
        PlayerDetection(creature);
        Patrol(creature);
    }

    public void PlayerDetection(Creature creature)
    {
        Collider[] inSightRange = Physics.OverlapSphere(creature.transform.position, creature.EnemyProperties.AgressionRange, creature.PlayerMask);

        if (inSightRange.Length == 0) return;
        creature.PlayerCharacter = inSightRange[0].gameObject;
        creature.SwitchState(creature.EnemyStateFactory.Chase);
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
