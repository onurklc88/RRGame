using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrajectoryDrawer : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    [SerializeField] private Vector3[] _points;
    private bool _drawing = false;
    public float TimeStep;
    public float mass;
    [SerializeField][Range(0,200)]
    private int _pointCount;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        //_points = new Vector3[20];
    }

    public void Draw(Vector3 startPos, Vector3 impulseForce)
    {
        if (!_drawing)
        {
            _points = new Vector3[_pointCount];
            _lineRenderer.positionCount = _pointCount;
            _drawing = true;
        }

        Vector3 velocity = impulseForce / mass;
        Vector3 currentPosition = startPos;

        float timeStep = (impulseForce.magnitude) * TimeStep / _pointCount;
        for (int i = 0; i < _pointCount; i++)
        {
            _points[i] = currentPosition;
            currentPosition += velocity * timeStep;
            velocity += new Vector3(0, -100, 0) * timeStep; // Apply gravity * 10
        }

        _lineRenderer.SetPositions(_points);
    }

    public void Clear()
    {
        _lineRenderer.positionCount = 0;
        _drawing = false;
    }
}
