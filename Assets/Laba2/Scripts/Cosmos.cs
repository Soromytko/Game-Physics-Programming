using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cosmos : MonoBehaviour
{
    [SerializeField] private CosmicBody[] _bodies;

    private void Awake()
    {
        _bodies = FindObjectsOfType<CosmicBody>();
    }

    private void Update()
    {
        for(int i = 0; i < _bodies.Length - 1; i++)
        {
            var body1 = _bodies[i];
            for (int j = i + 1; j < _bodies.Length; j++)
            {
                var body2 = _bodies[j];

            }
        }
    }

}
