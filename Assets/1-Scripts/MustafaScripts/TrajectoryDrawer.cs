using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrajectoryDrawer : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    [SerializeField] private Vector3[] _points;
    private MouseTarget _mouseTarget;
    private float _angleRadians;
    private bool _drawing = false;
    public float TimeStep;
    public float mass;
    [Range(0,200)]
    public int predictionSteps;

    void Start()
    {
        _mouseTarget = GetComponent<CharacterStateManager>().MouseTarget;
        _lineRenderer = GetComponent<LineRenderer>();
        //_points = new Vector3[20];
        _angleRadians = Mathf.Atan2(2.5f, 1.0f);
    }

    public void Draw(Vector3 startPos, Vector3 impulseForce)
    {
        //int predictionSteps = 50;
        //float timeStep = TimeStep; // Adjust the time step as needed
        int pointCount = predictionSteps;
        if (!_drawing)
        {
            //int pointCount = predictionSteps / 2;
            _points = new Vector3[pointCount];
            _lineRenderer.positionCount = pointCount;
            _drawing = true;
        }

        Vector3[] positions = new Vector3[pointCount];
        Vector3 velocity = impulseForce / mass;
        Vector3 currentPosition = startPos;

        float timeStep = (impulseForce.magnitude) * TimeStep / predictionSteps;
        for (int i = 0; i < pointCount; i++)
        {
            positions[i] = currentPosition;

            // Update the position and velocity using basic physics equations
            currentPosition += velocity * timeStep;
            velocity += new Vector3(0, -100, 0) * timeStep; // Apply gravity

            // If the trajectory goes below the ground, stop drawing
            /*
            if (currentPosition.y < startPos.y - 5.0f)
            {
                predictionSteps = i + 1;
                break;
            }
            */
        }

        _lineRenderer.positionCount = pointCount;
        _lineRenderer.SetPositions(positions);
    }

    public void Clear()
    {
        _lineRenderer.positionCount = 0;
        _drawing = false;
    }
}
