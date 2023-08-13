using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClimbState : CharacterBaseState
{
    private GameObject _ladder;
    public override void EnterState(CharacterStateManager character)
    {

        _ladder = character.GetComponent<CharacterCollisions>().Ladder;

        Debug.Log("Greetings from ClimbState");
        //character.StartCoroutine(DelayState(character));
        
    }
    public override void UpdateState(CharacterStateManager character)
    {
        if (Input.GetKey(KeyCode.W))
        {
            character.transform.Translate(Vector3.up * 6 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            character.transform.Translate(Vector3.up * -6 * Time.deltaTime);
        }


    }
    public override void ExitState(CharacterStateManager character)
    {
        Debug.Log("Quit climb state");
        character.SwitchState(character.CharacterStateFactory.CharacterIdleState);

    }

    public override IEnumerator DelayState(CharacterStateManager character)
    {
        yield return new WaitForSeconds(1f);
        ExitState(character);
    }


}
