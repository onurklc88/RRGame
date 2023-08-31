using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grimmort : Creature
{
    private IState _currentState = null;

    private void Start()
    {
        CreatureAttackType = EnemyStateFactory.MeleeAttack();
        LoadWayPoints();
        //SetPlayerLayer();
        SetCreatureProperties();
        _currentState = EnemyStateFactory.Walk();
    }
    private void Update()
    {
        ExecuteState();
    }
    protected override void SetCreatureProperties()
    {
        gameObject.GetComponent<SphereCollider>().radius = EnemyProperties.AgressionRange;
    }
    protected override void ExecuteState()
    {
        _currentState.ProcessState(this);
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
        _currentState = newState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            PlayerCharacter = other.gameObject;
            SwitchState(EnemyStateFactory.Chase());
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position + transform.forward * 1f, 5f);
    }
}
