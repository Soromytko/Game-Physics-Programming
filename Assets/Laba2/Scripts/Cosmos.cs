using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cosmos : MonoBehaviour
{
    public float time = 1f;
    public double AstronomicalUnit
    {
        get => _astronomicalUnit;
        set => _astronomicalUnit = value;
    }

    [SerializeField] private Planet[] _bodies;

    private double _astronomicalUnit;

    public Planet earth;
    public Planet apple;

    private void Awake()
    {
        _bodies = FindObjectsOfType<Planet>();
        _astronomicalUnit = CosmosConfig.AstronomicalUnit;
    }

    private void Test()
    {
        double g = GetFreeFallAcceleration(earth, apple);
    }

    private void Update()
    {
        Time.timeScale = time;
        //Test();
        //return;
        
        foreach(var body1 in _bodies)
        {
            foreach (var body2 in _bodies)
            {
                if (body1 == body2)
                {
                    continue;
                }
            }
        }
    }

    private double GetGravityForce(Planet body1, Planet body2)
    {
        //double G = CosmosConfig.G;
        //double m1 = body1.Mass;
        //double m2 = body2.Mass;
        //double R = Vector3.Distance(body1.transform.position, body2.transform.position);
        //double F = G * m1 * m2 / (R * R);

        double F = GetFreeFallAcceleration(body1, body2) * body2.Mass;

        return F;
    }

    private double GetFreeFallAcceleration(Planet body1, Planet body2)
    {
        double G = CosmosConfig.G;
        double R = Vector3.Distance(body1.transform.position, body2.transform.position);
        if (R <= body1.Radius * 0.5f + body2.Radius * 0.5f)
        {
            return 0f;
        }
        //R = (CosmosConfig.EarthRadius / 1000f / (CosmosConfig.AstronomicalUnit * 1000f)) * CosmosConfig.AstronomicalUnit;
        //print("R / _astronomicalUnit = " + R / _astronomicalUnit);
        R = To(R);
        double M = body1.Mass * CosmosConfig.EarthMass;
        double g = G * M / (R * R);
        print("R = " + R);

        int degree = CosmosConfig.GDegree + CosmosConfig.EarthMassDegree - CosmosConfig.AstronomicalUnitDegree * 2;
        return g * Math.Pow(10, degree);
    }

    private double To(double value)
    {
        return value / _astronomicalUnit * CosmosConfig.AstronomicalUnit;
        double result = value * CosmosConfig.EarthRadius * 2;
        //result /= (CosmosConfig.AstronomicalUnit * 1000f)) * CosmosConfig.AstronomicalUnit;
        return result;
    }
}
