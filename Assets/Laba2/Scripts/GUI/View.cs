using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    public void OnSimulationSpeedChanged(float value)
    {
        CosmosConfig.SimulationSpeed = value;
    }
}
