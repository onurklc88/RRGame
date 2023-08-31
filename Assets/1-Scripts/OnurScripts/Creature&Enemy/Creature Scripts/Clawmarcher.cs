using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clawmarcher : Creature
{
   
    private IState _currentState = null;
   
    private void Start()
    {
        LoadWayPoints();
        SetPlayerLayer();
        SetCreatureProperties();
        _currentState = EnemyStateFactory.Walk();
     
    }
    protected override void SetCreatureProperties()
    {
        gameObject.GetComponent<SphereCollider>().radius = EnemyProperties.AgressionRange;
    }
    private void Update()
    {
        ExecuteState();
    }


    protected override void ExecuteState()
    {
       _currentState.ProcessState(this);
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

    protected override void LoadWayPoints()
    {
       for(int i = 0; i < _splineComputer.pointCount; i++)
       {
            Waypoints.Add(_splineComputer.GetPoint(i).position);
       }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
            PlayerCharacter.transform.position = other.transform.position;
       
    }

    private void OnTriggerExit(Collider other)
    {
        SwitchState(EnemyStateFactory.Walk());
    }
    
}
