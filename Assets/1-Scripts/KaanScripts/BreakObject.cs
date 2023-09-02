using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    public List<Rigidbody> _childRB = new List<Rigidbody>();

    public List<Vector3> _childPos = new List<Vector3>();
    public List<Vector3> _childRot = new List<Vector3>();

    public Transform Player;

    void Start()
    {
        _childRB = GetComponentsInChildren<Rigidbody>().ToList();

        for (int i = 0; i < _childRB.Count; i++)
        {
            _childPos.Add(_childRB[i].transform.position); 
            _childRot.Add(_childRB[i].transform.eulerAngles); 
        }
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            BreakIt();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            FixIt();
        }
    }

    private void BreakIt()
    {
        var dir = transform.position - Player.transform.position;

        for (int i = 0; i < _childRB.Count; i++)
        {
            _childRB[i].isKinematic = false;
            _childRB[i].AddForce(dir * 20);
        }
    }

    private void FixIt()
    {
        for (int i = 0; i < _childRB.Count; i++)
        {
            _childRB[i].isKinematic = true;
            _childRB[i].DOMove(_childPos[i],2f).SetEase(Ease.InBack);
            _childRB[i].DORotate(_childRot[i],2f);
        }
    }


}
