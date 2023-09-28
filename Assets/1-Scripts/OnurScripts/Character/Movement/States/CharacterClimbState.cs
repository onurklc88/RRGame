using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using DG.Tweening;
using static UnityEditor.Experimental.GraphView.GraphView;
using static Dreamteck.Splines.ParticleController;
using static ToonyColorsPro.ShaderGenerator.Enums;

public class CharacterClimbState : CharacterBaseState
{

    private Ladder _ladder;
    private bool _openUpdate;
   

    public override void EnterState(CharacterStateManager character)
    {
        character.transform.GetChild(0).GetComponent<Animator>().SetBool("IsClimb", true);
        _ladder = character.GetComponent<CharacterCollisions>().TemporaryObject.transform.parent.GetComponent<Ladder>();
        _openUpdate = false;

        HoldLadder(character);


        Debug.Log("Greetings from ClimbState");
        //character.StartCoroutine(DelayState(character));
    }
    public override void UpdateState(CharacterStateManager character)
    {
        if (!_openUpdate) { return; }

        if (Input.GetKey(KeyCode.W))
        {
            character.transform.Translate(Vector3.up * 6 * Time.deltaTime);
            character.transform.GetChild(0).GetComponent<Animator>().SetFloat("ClimbFloat", 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            character.transform.Translate(Vector3.up * -6 * Time.deltaTime);
            character.transform.GetChild(0).GetComponent<Animator>().SetFloat("ClimbFloat", 1);
        }
        else
        {
            character.transform.GetChild(0).GetComponent<Animator>().SetFloat("ClimbFloat", 0);

        }

        if (character.transform.position.y > _ladder.UpExitLimit.position.y)
            UpExit(character);
        else if (character.transform.position.y < _ladder.DownExitLimit.position.y)
            DownExit(character);

    }
    public override void ExitState(CharacterStateManager character)
    {
        Debug.Log("Quit climb state");
        character.transform.GetChild(0).GetComponent<Animator>().SetBool("IsClimb", false);
        character.SwitchState(character.CharacterStateFactory.CharacterIdleState);

    }

    public override IEnumerator DelayState(CharacterStateManager character)
    {
        yield return new WaitForSeconds(1f);
        ExitState(character);
    }

    private void HoldLadder(CharacterStateManager character)
    {
        character.transform.DOMove(_ladder.EnterPoint.position, .2f);
        DOTween.To(() => character.transform.eulerAngles, x => character.transform.eulerAngles = x, _ladder.Rot, .2f).OnComplete(() =>
        {
            _openUpdate = true;
        });
    }

    private void UpExit(CharacterStateManager character)
    {
        character.transform.DOMove(_ladder.ExitPointUp.position, .2f);
        //character.transform.GetChild(0).GetComponent<Animator>().SetTrigger("ClimbEnd");
        character.transform.GetChild(0).GetComponent<Animator>().SetBool("IsClimb",false);
        //DOVirtual.DelayedCall(2.1f, () => { ExitState(character); });
        ExitState(character);

    }

    private void DownExit(CharacterStateManager character)
    {
        character.transform.DOMove(_ladder.ExitPointDown.position, .2f);
        DOTween.To(() => character.transform.eulerAngles, x => character.transform.eulerAngles = x, new Vector3(_ladder.Rot.x, _ladder.Rot.y + 180, _ladder.Rot.z), .2f);
        ExitState(character);
    }
}
