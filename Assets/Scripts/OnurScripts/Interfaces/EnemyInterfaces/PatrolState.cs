using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState
{
    public Vector3 center;
    
    private Vector3[] patrolPoints = new Vector3[4];
    private int currentPatrolPointIndex = 0;

  
    public void ProcessState(Creature creature)
    {
        Debug.Log("interface activated");
        SetRandomDestination(creature);
    }
    private void GeneratePatrolPoints(Creature creature)
    {
        Debug.Log("A");

        for (int i = 0; i < patrolPoints.Length; i++)
        {
            patrolPoints[i] = RandomNavmeshLocation(creature);
        }
    }
    private Vector3 RandomNavmeshLocation(Creature creature)
    {
        Vector3 randomDirection = Random.insideUnitSphere * 1f;
        randomDirection += center;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, creature.EnemyProperties.ChaseArea, NavMesh.AllAreas);
        return hit.position;
    }

    private void SetRandomDestination(Creature creature)
    {
        if (patrolPoints.Length == 0) GeneratePatrolPoints(creature);

        Debug.Log("patrol points length: " +patrolPoints.Length);
        int randomIndex = Random.Range(0, patrolPoints.Length);
        Debug.Log("RandomIndex: " + randomIndex);
        if (randomIndex == currentPatrolPointIndex)
        {
           randomIndex = (randomIndex + 1) % patrolPoints.Length;
        }

        
        currentPatrolPointIndex = randomIndex;
        creature.NavMeshAgent.SetDestination(patrolPoints[currentPatrolPointIndex]);
    }
}
