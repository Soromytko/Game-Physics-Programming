using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : CosmicBody
{
    [SerializeField] private Vector3 _movementDirection;
    [SerializeField] private float _acceleration;

    private Vector3 _gravitationForce;
    private Vector3 _forceDirection;

    private void FixedUpdate()
    {
        // F = ma
        Vector3 movementForce = _movementDirection.normalized * _mass * _acceleration; 
        _gravitationForce = CalculateGravitationForce();

        Vector3 force = movementForce + _gravitationForce;
        _forceDirection = force;

        transform.position += force * Time.fixedDeltaTime * CosmosConfig.SimulationSpeed * CosmosConfig.SimulationSpeedMultiplier;
    }

    private void OnDrawGizmos()
    {
        const float lineLength = 5;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + _movementDirection.normalized * lineLength);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + _gravitationForce.normalized * lineLength);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + _forceDirection.normalized * lineLength);
    }
}