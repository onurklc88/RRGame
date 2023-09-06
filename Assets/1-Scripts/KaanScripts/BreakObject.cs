using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    private List<Rigidbody> _childRB = new List<Rigidbody>();

    private List<Vector3> _childPos = new List<Vector3>();
    private List<Vector3> _childRot = new List<Vector3>();

    [SerializeField] private AnimationCurve curve;
    public Transform Player;

    void Start()
    {
        _childRB = GetComponentsInChildren<Rigidbody>().ToList();

        for (int i = 0; i < _childRB.Count; i++)
        {
            _childPos.Add(_childRB[i].transform.position); 
            _childRot.Add(_childRB[i].transform.eulerAngles); 
        }

        gameObject.SetActive(false);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            BreakIt();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            FixIt();
        }
    }

    public void BreakIt()
    {
        var dir = (transform.position - Player.transform.position).normalized;

        for (int i = 0; i < _childRB.Count; i++)
        {
            _childRB[i].isKinematic = false;
            _childRB[i].AddForce(dir * 30);
        }

        DOVirtual.DelayedCall(2, () =>
        {
            FixIt();
        });
    }

    private void FixIt()
    {
        for (int i = 0; i < _childRB.Count; i++)
        {
            _childRB[i].isKinematic = true;
            Sequence seq = DOTween.Sequence();

            //seq.Append(_childRB[i].transform.DOShakePosition(.4f,.3f,1));
            seq.Append(_childRB[i].DOMove(_childPos[i], 1f).SetEase(curve));
            seq.Join(_childRB[i].DORotate(_childRot[i], 1f));
            seq.AppendCallback(() =>
            {
                transform.parent.GetComponent<Collider>().enabled = true;
                transform.parent.GetChild(0).gameObject.SetActive(true);
                gameObject.SetActive(false);
            });
        }
    }

}
