using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Creature : MonoBehaviour
{
  
    [SerializeField] private EnemyProperties _enemyProperties;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private CreatureStates _creatureStates;
    private IState _currentState = null;
    public NavMeshAgent NavMeshAgent;
   

    #region Getters&Setter
    //getters and setters
    //public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public EnemyProperties EnemyProperties => _enemyProperties;
    public LayerMask PlayerMask => _playerMask;
    public IState CurrentState => _currentState;
    #endregion

    public abstract void SetCreatureProperties();
    public abstract void ExecuteState();

   


}
