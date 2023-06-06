using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "CreatureStates", menuName = "ScriptableObjects/Creatures", order = 1)]

public class CreatureStates : ScriptableObject
{
   

   // public List<Action> AddActionList() { SpawnState(), IdleState()}
   
    

    public void SwitchState(string newState) { }

    public void SpawnState(Creature creature)
    {

    }
    public void IdleState()
    {

    }
    public void PatrolState()
    {

    }
    public void ChaseState()
    {

    }
    public void AttackState()
    {

    }
    public void DieState()
    {

    }
}
