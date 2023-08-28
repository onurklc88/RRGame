using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Creature : MonoBehaviour
{
    [HideInInspector] public int CurrentWaypointIndex = 0;
    [HideInInspector] public GameObject PlayerCharacter;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private EnemyProperties _enemyProperties;
    [SerializeField] private float _idleDelayTime;
    private List<Vector3> _waypoints = new List<Vector3>();
    [SerializeField] protected SplineComputer _splineComputer;

    private LayerMask _playerLayer;
    
   
    [Inject]
    public EnemyStateFactory EnemyStateFactory;
    [Inject]
    public CharacterStateFactory CharacterStateFactory;

    #region Getters & Setters
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public List<Vector3> Waypoints => _waypoints;
    public EnemyProperties EnemyProperties => _enemyProperties;
    public float AnimationDelayTime => _idleDelayTime;
    public LayerMask PlayerMask => _playerLayer;
   
    #endregion

    public abstract void SetCreatureProperties();
    public abstract void ExecuteState();
    public abstract void SwitchState(IState newState);
    public virtual void SetPlayerLayer()
    {
        _playerLayer = LayerMask.GetMask("Player");
    }



}
