using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IState
{
    public void ProcessState(Creature creature)
    {
        Debug.Log("DieState");
    }
}
