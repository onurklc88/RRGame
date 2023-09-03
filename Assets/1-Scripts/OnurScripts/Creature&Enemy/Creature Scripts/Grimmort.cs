using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grimmort : Creature
{
   

    private void Start()
    {
        CreatureAttackType = EnemyStateFactory.MeleeAttack();
        LoadWayPoints();
        SetPlayerLayer();
        SetCreatureProperties();
        CurrentCreatureState = EnemyStateFactory.Walk();
    }
    private void Update()
    {
        ExecuteState();
    }
    protected override void SetCreatureProperties()
    {
        //gameObject.GetComponent<SphereCollider>().radius = EnemyProperties.AgressionRange;
    }
    protected override void ExecuteState()
    {
        CurrentCreatureState.ProcessState(this);
    }
    protected override void LoadWayPoints()
    {
        for (int i = 0; i < _splineComputer.pointCount; i++)
        {
            Waypoints.Add(_splineComputer.GetPoint(i).position);
        }
    }

    public override void SwitchState(IState newState)
    {
        newState.SetupState(this);
        CurrentCreatureState = newState;
    }

   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, EnemyProperties.AgressionRange);
    }
}
