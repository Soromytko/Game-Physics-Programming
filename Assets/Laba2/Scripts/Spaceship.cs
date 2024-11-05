using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : CosmicBody
{
    [SerializeField] private float _initialSpeed = 0.2f;
    [SerializeField] private float _rotationSpeed = 25f;
    [SerializeField] private float _acceleration = 0.01f;

    [SerializeField] private Vector3 _currentMovementForce;
    private Vector3 _gravitationForce;
    private Vector3 _forceDirection;

    private void Start()
    {
        _currentMovementForce = transform.forward * _initialSpeed;
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        bool is_acceleration = Input.GetKey(KeyCode.Space);
        
        transform.Rotate(Vector3.up, horizontal * _rotationSpeed * Time.fixedDeltaTime);
        
        _currentMovementForce += transform.forward * _acceleration * (is_acceleration ? 1f : 0f);
        _gravitationForce = CalculateGravitationForce();
        
        Vector3 force = _currentMovementForce + _gravitationForce;
        ApplyForce(force);
    }

    private void ApplyForce(Vector3 force)
    {
        transform.position += force * Time.fixedDeltaTime * CosmosConfig.SimulationSpeed * CosmosConfig.SimulationSpeedMultiplier;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + _currentMovementForce.normalized * 10f);
    }

}