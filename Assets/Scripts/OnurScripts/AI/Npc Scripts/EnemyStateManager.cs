using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour,IEnemy
{
    private void OnEnable()
    {
        EnemyHealth.OnEnemyDie += IdleState;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyDie -= IdleState;
    }

   public void IdleState()
   {
        Debug.Log("Im Dead");
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
}
