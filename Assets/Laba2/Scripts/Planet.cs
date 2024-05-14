using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float Mass
    {
        get => _mass;
        set => _mass = value;
    }
    public float Radius
    {
        get => _radius;
        set => _radius = value;
    }
    public float DistanceToSun
    {
        get => _distanceToSun;
        set => _distanceToSun = value;
    }
    public float DaysPerYear
    {
        get => _daysPerYear;
        set => _daysPerYear = value;
    }
    public Vector2 Deviation
    {
        get => _deviation;
        set => _deviation = value;
    }

    [Tooltip("In earth mass")]
    [SerializeField] private float _mass = 1f;
    [Tooltip("In earth radii")]
    [SerializeField] private float _radius = 1f;
    [Tooltip("In astronomical units")]
    [SerializeField] private float _distanceToSun = 1f;
    [SerializeField] private float _daysPerYear = 365f;
    [SerializeField] private Vector2 _deviation = new Vector2(1, 1);
    [SerializeField] private float _currentAngle = 0f;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _orbitSmoothness = 2;
    [SerializeField] private float _orbitThickness = 1f;

    private void Start()
    {
        if (_lineRenderer)
        {
            float distance = _distanceToSun * (float)CosmosConfig.AstronomicalUnit;
            int stepCount = (int)(distance) * _orbitSmoothness;
            DrawOrbit(stepCount, _distanceToSun * (float)CosmosConfig.AstronomicalUnit);
        }
    }

    public void Update()
    {
        float baseSimulationSpeed = CosmosConfig.SimulationSpeed * Time.deltaTime * 1000f;
        _currentAngle += baseSimulationSpeed / _daysPerYear;
        _currentAngle += _currentAngle < 0f ? 360f : _currentAngle >= 360 ? -360f : 0;
        float angleRad = Mathf.Deg2Rad * _currentAngle;

        float distance = _distanceToSun * (float)CosmosConfig.AstronomicalUnit;
        float x = Mathf.Cos(angleRad) * _deviation.x;
        float z = Mathf.Sin(angleRad) * _deviation.y;
        transform.localPosition = new Vector3(x, 0, z) * distance;

        if (_lineRenderer)
        {
            UpdateOrbitThickness();
        }
    }

    private void DrawOrbit(int stepCount, float distance)
    {
        _lineRenderer.positionCount = stepCount;

        for (int currentStep = 0; currentStep < stepCount; currentStep++) {
            float progress = currentStep / (float)stepCount;
            float angleRad = progress * Mathf.PI * 2f;
            
            float x = Mathf.Cos(angleRad) * Deviation.x;
            float z = Mathf.Sin(angleRad) * Deviation.y;

            Vector3 point = new Vector3(x, 0, z) * distance;

            _lineRenderer.SetPosition(currentStep, point);
        }
    }

    private void UpdateOrbitThickness()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 parentPosition = transform.parent != null ? transform.parent.position : transform.position;

        float distanceToSun = _distanceToSun * (float)CosmosConfig.AstronomicalUnit;
        float distanceToCamera = Vector3.Distance(cameraPosition, parentPosition);
        float distance = Mathf.Abs(distanceToCamera - distanceToSun);
        float width = distance / 100f * _orbitThickness;
        
        _lineRenderer.SetWidth(width, width);
    }
   
}
