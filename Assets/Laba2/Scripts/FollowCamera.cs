using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _sensitivity = 300f;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _deceleration = 0.5f;
    [SerializeField] private float _acceleration = 5f;
    private Vector2 _currentAngles;

    private void Update()
    {
        UpdateRotation();
    }

    private void FixedUpdate()
    {
        UpdatePosition();
    }

    private void UpdateRotation()
    {
        _currentAngles.x += Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        _currentAngles.y -= Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;

        _currentAngles.x += _currentAngles.x < 0f ? 360f : _currentAngles.x > 360f ? -360f : 0;
        _currentAngles.y = Mathf.Clamp(_currentAngles.y, -90f, 90f);

        transform.localEulerAngles = new Vector3(_currentAngles.y, _currentAngles.x, 0);
    }

    private void UpdatePosition()
    {
        if (_target) {
            transform.position = Vector3.Lerp(transform.position, _target.position, Time.fixedDeltaTime * 10f);
        }
    }

}
