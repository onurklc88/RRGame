using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterGecici : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if(other.name == "NpcCollider")
            {
                other.transform.parent.GetComponent<NpcDialogSystem>().LookDirection(transform);
                LookAtNpc(other.transform.parent.GetComponent<NpcDialogSystem>().Npc);
            }
        }
    }

    private void LookAtNpc(Transform npcTransform)
    {
        var vector = new Vector3(transform.position.x, npcTransform.position.y, transform.position.z);
        var dir = (npcTransform.position - vector).normalized;
        var targetRotation = Quaternion.LookRotation(dir, Vector3.up);

        transform.DORotateQuaternion(targetRotation, .3f).SetEase(Ease.OutSine);

    }
}
