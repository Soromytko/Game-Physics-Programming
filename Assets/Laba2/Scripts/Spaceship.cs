using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : CosmicBody
{
    [SerializeField] private float _initialSpeed = 0.2f;
    [SerializeField] private float _rotationSpeed = 25f;
    [SerializeField] private float _acceleration = 0.01f;

    private Vector3 _currentMovementForce;
    private Vector3 _gravitationForce;
    private Vector3 _forceDirection;

    private void Start()
    {
        _currentMovementForce = transform.forward;
        _currentMovementForce *= _initialSpeed;
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        bool is_acceleration = Input.GetKey(KeyCode.Space);
        
        if (is_acceleration) {
            _currentMovementForce += transform.forward * _acceleration;
        }

        transform.Rotate(Vector3.up, horizontal * _rotationSpeed * Time.fixedDeltaTime);
        transform.position += _currentMovementForce * Time.fixedDeltaTime;

        ApplyGravitation();

        return;
    }

    private void Move(Vector3 direction)
    {

    }

    private void ApplyGravitation()
    {
        _gravitationForce = CalculateGravitationForce();
        transform.position += _gravitationForce * Time.fixedDeltaTime * CosmosConfig.SimulationSpeed * CosmosConfig.SimulationSpeedMultiplier;
    }

 
}