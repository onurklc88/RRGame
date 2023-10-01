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

    void Start()
    {
        _mouseTarget = GetComponent<CharacterStateManager>().MouseTarget;
        _lineRenderer = GetComponent<LineRenderer>();
        //_points = new Vector3[20];
        _angleRadians = Mathf.Atan2(2.5f, 1.0f);
    }

    public void Draw(Vector3 startPos, Vector3 impulseForce)
    {
        int predictionSteps = 50;
        float timeStep = 0.1f; // Adjust the time step as needed
        if (!_drawing)
        {
            int pointCount = predictionSteps / 2;
            _points = new Vector3[pointCount];
            _lineRenderer.positionCount = pointCount;
            _drawing = true;
        }

        Vector3[] positions = new Vector3[predictionSteps / 2];
        Vector3 velocity = impulseForce;
        Vector3 currentPosition = startPos;

         
        for (int i = 0; i < predictionSteps / 2; i++)
        {
            positions[i] = currentPosition;

            // Update the position and velocity using basic physics equations
            currentPosition += velocity * timeStep;
            velocity += new Vector3(0, -100, 0) * timeStep; // Apply gravity

            // If the trajectory goes below the ground, stop drawing
            if (currentPosition.y < startPos.y - 5.0f)
            {
                predictionSteps = i + 1;
                break;
            }
        }

        _lineRenderer.positionCount = predictionSteps / 2;
        _lineRenderer.SetPositions(positions);
    }

    public void Clear()
    {
        _lineRenderer.positionCount = 0;
        _drawing = false;
    }
}
