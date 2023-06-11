using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestAI : MonoBehaviour
{
    public Vector3 center;
    public float patrolRange;
    private NavMeshAgent agent;
    private Vector3[] patrolPoints;
    private int currentPatrolPointIndex = 0;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GeneratePatrolPoints();
        SetRandomDestination();
    }

    private void Update()
    {
        // Hedefe ulaþtýysak, yeni bir rastgele hedef belirle
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetRandomDestination();
        }
    }

    private void GeneratePatrolPoints()
    {
        patrolPoints = new Vector3[4];
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            patrolPoints[i] = RandomNavmeshLocation();
        }
    }

    private Vector3 RandomNavmeshLocation()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRange;
        randomDirection += center;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, patrolRange, NavMesh.AllAreas);
        return hit.position;
    }

    private void SetRandomDestination()
    {
        // Rastgele bir devriye noktasý seç
        int randomIndex = Random.Range(0, patrolPoints.Length);
        if (randomIndex == currentPatrolPointIndex)
        {
            // Eðer seçilen nokta þu anki nokta ise bir sonraki noktayý seç
            randomIndex = (randomIndex + 1) % patrolPoints.Length;
        }

        // Yeni hedefi ayarla
        currentPatrolPointIndex = randomIndex;
        agent.SetDestination(patrolPoints[currentPatrolPointIndex]);
    }
}
