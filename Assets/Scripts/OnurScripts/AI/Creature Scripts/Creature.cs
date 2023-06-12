using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Creature : MonoBehaviour
{
    [HideInInspector] public int CurrentWaypointIndex = 0;
    [HideInInspector] public GameObject PlayerCharacter;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private EnemyProperties _enemyProperties;
    [SerializeField] private float _idleDelayTime;
    [SerializeField] private Transform[] _waypoint;
    private IState _currentState;
    private StateFactory _stateFactory = new StateFactory();
    
    
    #region Getters & Setters
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public Transform[] Waypoint => _waypoint;
    public EnemyProperties EnemyProperties => _enemyProperties;
    public float AnimationDelayTime => _idleDelayTime;
    public StateFactory StateFactory => _stateFactory;
   
    public IState CurrentState => _currentState;

    #endregion

    public abstract void SetCreatureProperties();
    public abstract void ExecuteState();
    public abstract void SwitchState(IState newState);

   


}
