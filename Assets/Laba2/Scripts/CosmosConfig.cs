using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class CosmosConfig
{
    public static readonly decimal G = 6.67300m;
    public static readonly decimal EarthMass = 5.9726m;
    public static readonly decimal EarthRadius = 6371000m; // m
    // public static readonly decimal EarthRadius = 0.00235335185471m; // m * 10^6
    public static readonly decimal AstronomicalUnit = 1.49597870700m;
    public static readonly float DistanceToSun = 30f;
    public static readonly float SimulationSpeedMultiplier = 100f;
    public static float SimulationSpeed { get; set; } = 1f;

    public static readonly int GDegree = -11;
    public static readonly int EarthMassDegree = 24;
    public static readonly int AstronomicalUnitDegree = 11;


}
