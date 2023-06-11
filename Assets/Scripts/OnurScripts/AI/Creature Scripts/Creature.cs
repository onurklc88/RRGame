using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Creature : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private EnemyProperties _enemyProperties;
    [SerializeField] private float _idleDelayTime;
    [SerializeField] private Transform[] _waypoint;
    [HideInInspector] public int CurrentWaypointIndex = 0;
    private StateFactory _stateFactory = new StateFactory();
    #region Getters & Setters
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public Transform[] Waypoint => _waypoint;
    public EnemyProperties EnemyProperties => _enemyProperties;
    public float AnimationDelayTime => _idleDelayTime;
    public StateFactory StateFactory => _stateFactory;
 
    #endregion

    public abstract void SetCreatureProperties();
    public abstract void ExecuteState();
    public abstract void SwitchState(IState newState);

   


}
