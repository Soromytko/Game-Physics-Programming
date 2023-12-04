using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class SolarSystem : MonoBehaviour
{
    public float SimulationSpeed
    {
        get => _simulationSpeed;
        set => _simulationSpeed = value;
    }
    public float AstronomicalUnit
    {
        get => _astronomicalUnit;
        set => _astronomicalUnit = value;
    }
    public float EarthRadius
    {
        get => _earthRadius;
        set => _earthRadius = value;
    }

    [SerializeField] private float _simulationSpeed = 1f;   
    [SerializeField] private float _astronomicalUnit = 30f;
    [SerializeField] private float _earthRadius = 1f;
    [SerializeField] private Planet[] _planets;


    public void UpdatePlanets()
    {
        foreach (var planet in _planets)
        {
            planet.transform.position = Vector3.right * planet.DistanceToSun * AstronomicalUnit;
            planet.transform.localScale = Vector3.one * planet.Radius;
        }
        CosmosConfig.AstronomicalUnit = AstronomicalUnit;

        FindObjectOfType<Cosmos>().AstronomicalUnit = AstronomicalUnit;
    }

    private void Start()
    {
        UpdatePlanets();
    }

}
