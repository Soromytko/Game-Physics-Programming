using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 300f;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _deceleration = 0.5f;
    [SerializeField] private float _acceleration = 5f;

[SerializeField]
    private Vector2 _currentAngles;

    private void Start()
    {
        _currentAngles.x = transform.localEulerAngles.y;
        _currentAngles.y = transform.localEulerAngles.x;
    }

    private void Update()
    {
        UpdateRotation();
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
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            direction -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += transform.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction -= transform.forward;
        }
        if (Input.GetKey(KeyCode.W))
        {
            direction += transform.forward;
        }

        float speed = _speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= _acceleration;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            speed *= _deceleration;
        }

        transform.position += direction * speed * Time.deltaTime;
    }
}
