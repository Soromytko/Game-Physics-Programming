using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardKinematics : MonoBehaviour
{
    [SerializeField] protected Transform _head;
    [SerializeField] protected Joint[] _joints;
    [SerializeField] protected float[] _angles;

    private void Start()
    {
        if (_angles.Length != _joints.Length)
        {
            _angles = new float[_joints.Length];
        }
    }

    private void Update()
    {
        _head.position = CalculateForwardKinematicPosition();
        ApplyAngles();
    }

    protected Vector3 CalculateForwardKinematicPosition()
    {
        Vector3 result = _joints[0].transform.position;
        Quaternion rotation = Quaternion.identity;
        for (int i = 1; i < _joints.Length; i++)
        {
            rotation *= Quaternion.AngleAxis(_angles[i - 1], _joints[i - 1].RotationAxis);
            result += rotation * _joints[i].transform.localPosition;
        }
        return result;
    }

    protected void ApplyAngles()
    {
        for (int i = 0; i < _joints.Length; i++)
        {
            _joints[i].Angle = _angles[i];
        }
    }
}
