using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{

    public void SetupState(Creature creature)
    {
        Debug.Log("Chase State");
        creature.NavMeshAgent.speed = 5;
    }
    public void ProcessState(Creature creature)
    {
        ChasePlayer(creature);
    }

    private void ChasePlayer(Creature creature)
    {
        creature.NavMeshAgent.SetDestination(creature.PlayerCharacter.transform.position);
    }
}

