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
    }
   
}
