using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : CosmicBody
{
    [SerializeField] private Vector3 _movementDirection;
    [SerializeField] private float _acceleration;

    private Vector3 _forceDirection;

    private void FixedUpdate()
    {
        // F = ma
        Vector3 movementForce = _movementDirection.normalized * _mass * _acceleration; 
        Vector3 gravitationForce = CalculateGravitationForce();

        Vector3 force = movementForce + gravitationForce;
        _forceDirection = force.normalized;

        transform.position += force * Time.fixedDeltaTime * CosmosConfig.SimulationSpeed * CosmosConfig.SimulationSpeedMultiplier;
    }

    private void OnDrawGizmos()
    {
        const float lineLength = 5;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + _movementDirection.normalized * lineLength);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + _forceDirection * lineLength);
    }
}