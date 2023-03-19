using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBaseState
{
    public CharacterStateManager CharacterManager;

    public abstract void EnterState();
    public abstract void Update();
    public abstract void ExitState();

    public virtual void Collision(CharacterStateManager character, Collider collider)
    {

    }

    public virtual IEnumerator DelayState(CharacterStateManager character)
    {
        yield return null;
    }



}
