using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public void ProcessState(Creature creature)
    {
        Debug.Log("AttackState");
    }
}
