using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{

    public void SetupState(Creature state)
    {

    }
    public void ProcessState(Creature state)
    {
        Debug.Log("ChaseState");
    }
}

