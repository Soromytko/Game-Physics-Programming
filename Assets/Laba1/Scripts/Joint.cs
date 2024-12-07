using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint : MonoBehaviour
{
    public Vector3 RotationAxis { get => _rotationAxis; private set => _rotationAxis = value; }
    public Quaternion InitialRotation {get => _initialRotation; private set => _initialRotation = value; }
    public float Angle {
        get => _angle;
        set
        {
            _angle = value;
            // transform.localRotation = Quaternion.AngleAxis(value, _rotationAxis);
            transform.localRotation = CalcRotation(_angle);
        }
    }

    [SerializeField] private Vector3 _rotationAxis = Vector3.right;
    private Quaternion _initialRotation;
    
    private float _angle = 0f;

    private void Start()
    {
        _initialRotation = transform.localRotation;
    }

    public Quaternion CalcRotation(float angle)
    {
        // return _initialRotation * Quaternion.Euler(axis * angle);
        return _initialRotation * Quaternion.Euler(_rotationAxis * angle);
    }

    public void ApplyAngle(float angle, Vector3 axis)
    {
        // transform.localRotation = CalcRotation(angle, axis);
    }
}
