using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpLadder : MonoBehaviour
{
    [SerializeField] private Transform _exitPoint;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        if (other.GetComponent<CharacterStateManager>().IsLadder)
    //        {
    //            other.transform.DOMove(_exitPoint.position, .2f);
    //            DOVirtual.DelayedCall(.2f, () =>
    //            {
    //                other.GetComponent<CharacterStateManager>().IsLadder = false;
    //            });
    //        }
            
    //    }
    //}
}
