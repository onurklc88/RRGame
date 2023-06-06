using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Creature : MonoBehaviour
{
    public enum CreatureBehaviour
    {
        Idle,
        Chase,
        Attack
    }
    [SerializeField] private EnemyProperties _enemyProperties;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private CreatureStates _creatureStates;
    private CreatureBehaviour _currentState;
    //getters and setters
    public EnemyProperties EnemyProperties => _enemyProperties;
    public LayerMask PlayerMask => _playerMask;
    public CreatureStates CreatureStates => _creatureStates;
    public CreatureBehaviour CurrentState => _currentState;

    public abstract void SetCreatureProperties();
    public abstract void ExecuteState();

   

    
}
