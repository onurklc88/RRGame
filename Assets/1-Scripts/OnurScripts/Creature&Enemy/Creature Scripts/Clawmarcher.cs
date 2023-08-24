using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clawmarcher : Creature
{
   
    private IState _currentState = null;
    
    private void Start()
    {
        SetPlayerLayer();
        SetCreatureProperties();
        _currentState = StateFactory.Walk();
     
    }
    public override void SetCreatureProperties()
    {
        gameObject.GetComponent<SphereCollider>().radius = EnemyProperties.AgressionRange;
    }
    private void Update()
    {
        ExecuteState();
    }


    public override void ExecuteState()
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
            SwitchState(StateFactory.Chase());
        }
           
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
            PlayerCharacter.transform.position = other.transform.position;
       
    }

    private void OnTriggerExit(Collider other)
    {
        SwitchState(StateFactory.Walk());
    }
    
}
