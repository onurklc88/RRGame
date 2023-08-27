using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureDieState : IState
{
    public void SetupState(Creature state)
    {

    }
    public void ProcessState(Creature creature)
    {
        Debug.Log("DieState");
    }
}
