using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Dreamteck.Splines;

[RequireComponent(typeof(NavMeshAgent))]
public class SplineTest : MonoBehaviour
{
    private NavMeshAgent _navMeshagent;
    [SerializeField] private SplineComputer splineComputer;
    public List<Vector3> _path = new List<Vector3>();
    public int CurrentIndex = 0;
    private void Awake()
    {
        _navMeshagent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        for(int i = 0; i < splineComputer.pointCount; i++)
        {
           
            Debug.Log(splineComputer[i].position);
            _path.Add(splineComputer.GetPoint(i).position);
            
        }
    }

    private void Update()
    {
        TestEn();

      
    }

    private void TestEn()
    {
        if (transform.position.z != _path[CurrentIndex].z)
        {
            Debug.Log("A");
            _navMeshagent.SetDestination(_path[CurrentIndex]);
        }
        else
        {
            Debug.Log("B");
           
            CurrentIndex = (CurrentIndex + 1) % _path.Count;

        }
    }
}
