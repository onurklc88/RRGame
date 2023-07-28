using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DownLadder : MonoBehaviour
{
    [SerializeField] private Vector3 _turnRot;
    [SerializeField] private Transform _enterPoint;
    [SerializeField] private Transform _exitPoint;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            { 
                HoldLadder(other.transform);
            }

            if(other.GetComponent<CharacterLadderControl>().IsLadder)
            {
                if (other.transform.position.y + .1f < _enterPoint.position.y)
                {
                    ExitLadderDown(other.transform);
                }
            }
            
        }
    }

    private void HoldLadder(Transform _player)
    {
        _player.DOMove(_enterPoint.position, .2f);
        DOTween.To(() => _player.transform.eulerAngles, x => _player.transform.eulerAngles = x, _turnRot, .2f).OnComplete(() =>
        {
            _player.GetComponent<CharacterStateManager>().enabled = false;
            _player.GetComponent<CharacterLadderControl>().IsLadder = true;
        });
    }

    private void ExitLadderDown(Transform _player)
    {
        _player.DOMove(_exitPoint.position, .2f);
        DOTween.To(() => _player.transform.eulerAngles, x => _player.transform.eulerAngles = x, new Vector3(_turnRot.x,_turnRot.y + 180, _turnRot.z), .2f);
        _player.GetComponent<CharacterStateManager>().enabled = true;
        _player.GetComponent<CharacterLadderControl>().IsLadder = false;
    }
}
