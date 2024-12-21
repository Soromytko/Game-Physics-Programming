using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : CosmicBody
{
    [SerializeField] private float _initialSpeed = 0.2f;
    [SerializeField] private float _rotationSpeed = 25f;
    [SerializeField] private float _acceleration = 0.01f;

    [SerializeField] private Vector3 _currentMovementForce;
    [SerializeField] private Vector3 _gravitationForce;
    [SerializeField] private Vector3 _velocity;

    private void Start()
    {
        _velocity = transform.forward * _initialSpeed;
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        bool is_acceleration = Input.GetKey(KeyCode.Space);
        
        transform.Rotate(Vector3.up, horizontal * _rotationSpeed * Time.fixedDeltaTime);
        
        _currentMovementForce = transform.forward * _acceleration * (is_acceleration ? 1f : 0f);
        _gravitationForce = CalculateGravitationForce();
        
        float delta = Time.fixedDeltaTime * CosmosConfig.SimulationSpeed * CosmosConfig.SimulationSpeedMultiplier;
        delta /= 10f;
        Vector3 force = _currentMovementForce + _gravitationForce;

        _velocity += force * delta;
        transform.position += _velocity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + _velocity * 50f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + _gravitationForce * 5000f);
    }

}