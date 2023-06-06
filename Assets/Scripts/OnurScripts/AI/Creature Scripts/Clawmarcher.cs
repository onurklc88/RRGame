using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clawmarcher : Creature
{
    private bool _playerDetection;
    
    private void Start()
    {
        CurrentState = CreatureStates.CreatureState.Idle;
        SetCreatureProperties();
    }

    private void Update()
    {
        ExecuteState(CurrentState);
    }


    public override void ExecuteState(CreatureStates.CreatureState state)
    {
        switch (state)
        {
            case CreatureStates.CreatureState.Idle:
              
                break;
            case CreatureStates.CreatureState.Patrol:
                Debug.Log("Patrol");
                break;
            case CreatureStates.CreatureState.Chase:
                Debug.Log("Chase");
                break;
            case CreatureStates.CreatureState.Attack:
                Debug.Log("Attack");
                break;
            case CreatureStates.CreatureState.Die:
                break;
        }
    }

    public override void SetCreatureProperties()
    {
        gameObject.GetComponent<SphereCollider>().radius = EnemyProperties.ChaseArea;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
            CurrentState = CreatureStates.CreatureState.Chase;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
             CheckDistanceBetweenPlayer(other.transform.position);
        
       
    }

    private void OnTriggerExit(Collider other)
    {
        CurrentState = CreatureStates.CreatureState.Patrol;
    }

    private void CheckDistanceBetweenPlayer(Vector3 playerPosition)
    {
         float distanceBetweenPlayer = Vector3.Distance(playerPosition, transform.position);

        if (distanceBetweenPlayer < 4f)
            CurrentState = CreatureStates.CreatureState.Attack;
         

    }
    


}
