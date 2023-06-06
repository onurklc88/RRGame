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
    public CreatureStates.CreatureState CurrentState;
    //getters and setters
    public EnemyProperties EnemyProperties => _enemyProperties;
    public LayerMask PlayerMask => _playerMask;
    public CreatureStates CreatureStates => _creatureStates;
   

    public abstract void SetCreatureProperties();
    public abstract void ExecuteState(CreatureStates.CreatureState state);

   

    
}
