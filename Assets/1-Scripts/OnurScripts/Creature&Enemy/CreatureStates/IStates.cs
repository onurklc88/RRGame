using UnityEngine;
public interface IState
{
    
    void SetupState(Creature creature);
    void ProcessState(Creature creature) { }
    void PlayerDetection(Creature creature) { }
}

