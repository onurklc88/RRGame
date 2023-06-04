using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStateManager : MonoBehaviour,IStates
{
    private void OnEnable()
    {
       
    }

    private void OnDisable()
    {
       
    }




    #region States
    public void SpawnState()
    {

    }
    public void IdleState()
    {

    }
    public void PatrolState()
    {

    }
    public void ChaseState()
    {

    }
    public void AttackState()
    {

    }
    public void DieState()
    {

    }

    #endregion
}
