using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clawmarcher : Creature
{
   
    private IState _currentState = null;
    



    private void Start()
    {
        _currentState = StateFactory.Walk();
       // _currentState.ProcessState(this);
    }
    public override void SetCreatureProperties()
    {
        gameObject.GetComponent<SphereCollider>().radius = EnemyProperties.ChaseArea;
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
            SwitchState(StateFactory.Chase());
        }
           
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
             CheckDistanceBetweenPlayer(other.transform.position);
        
       
    }

    private void OnTriggerExit(Collider other)
    {
        SwitchState(StateFactory.Walk());
    }

    private void CheckDistanceBetweenPlayer(Vector3 playerPosition)
    {
         float distanceBetweenPlayer = Vector3.Distance(playerPosition, transform.position);

        if (distanceBetweenPlayer < 4f)
        {
            SwitchState(StateFactory.Attack());
        }
           
    }
    


}
