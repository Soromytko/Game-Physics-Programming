using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public Vector3 Velocity
    {
        get => _velocity;
        set => _velocity = value;
    }
    public float Mass
    {
        get => _mass;
        set => _mass = value;
    }

    public Vector3 AstronomicalUnitPosition
    {
        get => transform.position;
        set => transform.position = value;
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

    [SerializeField] private float _hoursPerDay = 24f;
    [SerializeField] private float _daysPerYear = 365f;
    [Tooltip("In earth mass")]
    [SerializeField] private float _mass = 1f;
    [Tooltip("In earth radii")]
    [SerializeField] private float _radius = 1f;
    [Tooltip("In astronomical units")]
    [SerializeField] private float _distanceToSun = 1f;

    [SerializeField] private Vector3 _velocity;


    public void Move(Vector3 force)
    {
        Vector3 velocity = _velocity + force;
        transform.position += _velocity;
    }

    private void Start()
    {
        
    }

    private void Update()
    {

    }

    private void LateUpdate()
    {
        transform.position += _velocity * Time.deltaTime;
    }
}
