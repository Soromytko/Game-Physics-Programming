using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint : MonoBehaviour
{
    public Vector3 RotationAxis { get => _rotationAxis; private set => _rotationAxis = value; }

    [SerializeField] private Vector3 _rotationAxis = Vector3.right;
}
