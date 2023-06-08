using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clawmarcher : Creature
{
    private bool _playerDetection;
    [SerializeField] IState _idleState = new IdleState();
    [SerializeField] IState _patrolState = new PatrolState();
    [SerializeField] IState _chaseState = new ChaseState();
    [SerializeField] IState _attackState = new AttackState();
    private IState _currentState = null;
    private void Start()
    {
        _currentState = _patrolState;
       // _currentState.ProcessState(this);
    }

    private void FixedUpdate()
    {
        _currentState.ProcessState(this);
    }


    public override void ExecuteState()
    {
       _currentState.ProcessState(this);
    }

    public override void SetCreatureProperties()
    {
       gameObject.GetComponent<SphereCollider>().radius = EnemyProperties.ChaseArea;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6) { }
           
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
             CheckDistanceBetweenPlayer(other.transform.position);
        
       
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    private void CheckDistanceBetweenPlayer(Vector3 playerPosition)
    {
         float distanceBetweenPlayer = Vector3.Distance(playerPosition, transform.position);

        if (distanceBetweenPlayer < 4f)
        {

        }
           
    }
    


}
