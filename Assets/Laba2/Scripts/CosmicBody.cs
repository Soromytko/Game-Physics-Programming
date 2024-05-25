using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmicBody : MonoBehaviour
{
    public float Mass
    {
        get => _mass;
        set => _mass = value;
    }

    [Tooltip("In earth mass")]
    [SerializeField] protected float _mass = 1f;
    private static List<CosmicBody> _bodies = new List<CosmicBody>();

    private void OnEnable()
    {
        _bodies.Add(this);
    }

    private void OnDisable()
    {
        _bodies.Remove(this);
    }

    protected Vector3 CalculateGravitationForce()
    {
        Vector3 result = new Vector3();
        foreach (var body in _bodies)
        {
            if (body != this)
            {
                result += CalculateGravitationForce(body);
            }
        }
        return result;
    }

    private Vector3 CalculateGravitationForce(CosmicBody body)
    {
        // Newton's law of universal gravitation: F = G * m1 * m2 / R^2
        decimal G = CosmosConfig.G;
        decimal m1 = (decimal)_mass * CosmosConfig.EarthMass;
        decimal m2 = (decimal)body.Mass * CosmosConfig.EarthMass;
        decimal R = (decimal)Vector3.Distance(transform.position, body.transform.position) / (decimal)CosmosConfig.DistanceToSun * CosmosConfig.AstronomicalUnit;
        decimal F = G * m1 * m2 / (R * R);
        int FDegree = CosmosConfig.GDegree + CosmosConfig.EarthMassDegree * 2 - CosmosConfig.AstronomicalUnitDegree * 2;
        // Subtracts 3, because we divide by the mass below
        F = F * (decimal)Math.Pow(10, FDegree - CosmosConfig.EarthMassDegree);

        // Newton's Second Law: F = ma     
        decimal a = F / m1;

        Vector3 forceDirection = (body.transform.position - transform.position).normalized;
        return forceDirection * (float)(a / CosmosConfig.AstronomicalUnit * (decimal)CosmosConfig.DistanceToSun) * _mass;
    }

}
