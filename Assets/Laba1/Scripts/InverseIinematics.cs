using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InverseIinematics : ForwardKinematics
{
    [SerializeField] private float _sampling = 2f;
    [SerializeField] private float _rate = 12f;
    [SerializeField] private Transform _target;

    private void Update()
    {
        ProcessInverseKinematic();
        ApplyAngles();
    }

    private void ProcessInverseKinematic()
    {
        for (int i = 0; i < _angles.Length; i++)
        {
            float angle = _angles[i];

            float f1 = GetTargetApproximation();
            _angles[i] += _sampling;
            float f2 = GetTargetApproximation();
            float gradient = (f1 - f2) / _sampling;

            _angles[i] = angle + _rate * gradient;
        }

    }

    private float GetTargetApproximation()
    {
        Vector3 point = CalculateForwardKinematicPosition();
        return Vector3.Distance(point, _target.transform.position);
    }

}
