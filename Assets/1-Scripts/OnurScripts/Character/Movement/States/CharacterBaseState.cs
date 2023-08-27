using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBaseState
{
    public virtual void EnterState(CharacterStateManager character) { }
    public virtual void UpdateState(CharacterStateManager character) { }
    public abstract void ExitState(CharacterStateManager character);
    public virtual IEnumerator DelayState(CharacterStateManager character)
    {
        yield return null;
    }

    public virtual void ApplyDashGravity(CharacterStateManager character)
    {

    }



}
