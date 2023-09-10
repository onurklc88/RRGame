using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThrowArrow : CharacterAttackState
{
    public override void EnterState(CharacterStateManager character)
    {
      
        character.CharacterContainer.ColorTube.SetActive(true);
        character.CharacterContainer.ColorTube.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(character.CharacterContainer.ColorTube.GetComponent<SkinnedMeshRenderer>().sharedMesh.GetBlendShapeIndex("Squezed"), 1f);
    }
    public override void UpdateState(CharacterStateManager character)
    {
        TrackCursorPosition(character);
    }
    public override void DoAttackBehaviour(CharacterStateManager character)
    {
      if (!character.WeaponHandler.IsChargeReady()) { ExitState(character); return; }
       
       GameObject arrow = ObjectPool.GetPooledObject(0);
       arrow.transform.position = character.transform.position + character.transform.forward;
       arrow.SetActive(true);
       Vector3 arrowMovePosition = arrow.transform.position + character.transform.forward * 40f;
       arrow.transform.DOMove(arrowMovePosition, 0.5f).OnComplete(() => EventLibrary.ResetPooledObject.Invoke(arrow));
       EventLibrary.OnWeaponChargeUpdated.Invoke(false);
        character.StartCoroutine(DelayState(character));
      
    }

    public override void ExitState(CharacterStateManager character)
    {
        character.CharacterContainer.ColorTube.SetActive(false);
        character.SwitchState(character.CharacterStateFactory.CharacterIdleState);
    }

    public override IEnumerator DelayState(CharacterStateManager character)
    {
        float startTime = Time.time;
        while (Time.time < startTime + 0.25)
        {
            float targetValue = Mathf.Lerp(0f, 100.0f, 1f * 4);
            character.CharacterContainer.ColorTube.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(character.CharacterContainer.ColorTube.GetComponent<SkinnedMeshRenderer>().sharedMesh.GetBlendShapeIndex("Squezed"), targetValue);
            yield return null;
        }
        ExitState(character);
    }
}
