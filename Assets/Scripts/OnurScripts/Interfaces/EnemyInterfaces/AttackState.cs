using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private Vector3 _dashPosition;
    private Vector3 _playerPosition;
    public void SetupState(Creature creature)
    {
        _playerPosition = creature.PlayerCharacter.transform.position;
    }
    public void ProcessState(Creature creature)
    {
        Debug.Log("AttackState");
    }

    private void AttackPlayer(Creature creature)
    {
        float startTime = Time.time;
        while (Time.time < startTime + 0.25)
        {
            creature.NavMeshAgent.Move(_playerPosition * Time.deltaTime);

            /*
            if (character.CurrentMove.magnitude < 1f)
                _dashPosition = character.transform.position + character.transform.forward * 15f;
            else
                _dashPosition = character.transform.position + character.CurrentMove * 15f;



            character.CharacterController.Move((_dashPosition - character.transform.position) * Time.deltaTime);


            yield return null;
            */
        }
    }

    private IEnumerator DelayState(Creature creature)
    {
        Debug.Log("IDLE DELAY");
        yield return new WaitForSeconds(creature.AnimationDelayTime);
        creature.SwitchState(creature.StateFactory.Chase());
    }
}
