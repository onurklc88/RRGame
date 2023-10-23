using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using DG.Tweening;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Creature : MonoBehaviour
{
    public IState CreatureAttackType { get; set; }
    public IState CurrentCreatureState { get; set; }
    
    [HideInInspector] public int CurrentWaypointIndex = 0;
    [HideInInspector] public GameObject PlayerCharacter;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private CreatureProperties _enemyProperties;
    [SerializeField] private float _idleDelayTime;
    [SerializeField] private CreatureAnimationController _creatureAnimationController;
    [SerializeField] protected SplineComputer _splineComputer;
    [SerializeField] private DissolveHandler _dissolveHandler;
    private List<Vector3> _waypoints = new List<Vector3>();
    [SerializeField] private LayerMask _playerLayer;
   
   
    [Inject]
    public EnemyStateFactory EnemyStateFactory;
    [Inject]
    public CharacterStateFactory CharacterStateFactory;

    #region Getters & Setters
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public List<Vector3> Waypoints => _waypoints;
    public CreatureProperties EnemyProperties => _enemyProperties;
    public float AnimationDelayTime => _idleDelayTime;
    public LayerMask PlayerMask => _playerLayer;
    public CreatureAnimationController CreatureAnimationController => _creatureAnimationController;
    public DissolveHandler DissolveHandler => _dissolveHandler;
    #endregion
    public abstract void SwitchState(IState newState);
    protected abstract void SetCreatureProperties();
    protected abstract void LoadWayPoints();
    protected abstract void ExecuteState();
   
    protected virtual void SetPlayerLayer()
    {
        _playerLayer = LayerMask.GetMask("Player");
    }
    
}
