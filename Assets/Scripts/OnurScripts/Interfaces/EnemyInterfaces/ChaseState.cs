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
        CheckDistanceBetweenPlayer(creature);
        ChasePlayer(creature);
    }

    private void ChasePlayer(Creature creature)
    {
        creature.NavMeshAgent.SetDestination(creature.PlayerCharacter.transform.position);
    }
    private void CheckDistanceBetweenPlayer(Creature creature)
    {
        if (creature.PlayerCharacter == null) return;

        float distanceBetweenPlayer = Vector3.Distance(creature.PlayerCharacter.transform.position, creature.transform.position);

        if (distanceBetweenPlayer < 7f)
            creature.SwitchState(creature.StateFactory.Attack());
    }
}

