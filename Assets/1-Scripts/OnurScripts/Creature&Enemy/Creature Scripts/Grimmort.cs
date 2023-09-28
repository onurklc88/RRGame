using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grimmort : Creature
{

    private void Awake()
    {
        CreatureAttackType = EnemyStateFactory.MeleeAttack();
        LoadWayPoints();
        SetPlayerLayer();
        //SetCreatureProperties();
        CurrentCreatureState = EnemyStateFactory.Walk();
    }

  
    private void Update()
    {
        ExecuteState();
    }
    protected override void SetCreatureProperties()
    {
        //DamageTween = transform.DOJump(transform.position + -transform.forward * 7f, 1f, 1, 0.5f);
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
        CurrentCreatureState = newState;
        newState.SetupState(this);
    }

   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, EnemyProperties.AgressionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z), new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z) + -transform.forward * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        
    }
}
