using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint : MonoBehaviour
{
    public Vector3 RotationAxis { get => _rotationAxis; private set => _rotationAxis = value; }
    public float Angle {
        get => _angle;
        set
        {
            _angle = value;
            transform.localRotation = Quaternion.AngleAxis(value, _rotationAxis);
        }
    }

    [SerializeField] private Vector3 _rotationAxis = Vector3.right;
    
    private float _angle = 0f;
}
