using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBaseState
{
    public abstract void EnterState(CharacterStateManager character);
    public abstract void UpdateState(CharacterStateManager character);
    public abstract void ExitState(CharacterStateManager character);

    public virtual void Collision(CharacterStateManager character, Collider collider)
    {

    }

    public virtual IEnumerator DelayState(CharacterStateManager character)
    {
        yield return null;
    }



}
