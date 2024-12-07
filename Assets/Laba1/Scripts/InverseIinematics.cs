using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InverseIinematics : MonoBehaviour
{
    [SerializeField] private float _sampling = 359f;
    [SerializeField] private float _rate = 2000f;
    [SerializeField] private float _distanceThreshold = 0.01f;
    [SerializeField] private Vector3 _armatureScale = Vector3.one;
    [SerializeField] private Transform _target;
    [SerializeField] private Joint[] _joints;

    [SerializeField] private float[] _angles;

    private void Start()
    {
        _angles = new float[_joints.Length];
    }

    private void Update()
    {
        // _joints[0].transform.localRotation *= Quaternion.Euler(Time.deltaTime * 10, 0, 0);
        // return;
        ProcessInverseKinematic();
        ProcessInverseKinematic();
        ProcessInverseKinematic();
        ApplyAngles();
    }

    private void OnDrawGizmos()
    {
        if (!UnityEditor.EditorApplication.isPlaying) {
            return;
        }
        Vector3 fkTargetPosition = CalculateForwardKinematicPosition();
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(fkTargetPosition, 0.2f);
    }

    private void ProcessInverseKinematic()
    {
        for (int i = 0; i < _angles.Length; i++)
        {
            float angle = _angles[i];

            float f = GetTargetApproximation();

            // Terminate prematurely if the target is reached
            if (f <= _distanceThreshold) {
                return;
            }

            _angles[i] += _sampling;
            float fd = GetTargetApproximation();
            float gradient = (f - fd) / _sampling;

            _angles[i] = angle - _rate * gradient;
        }

    }

    private Vector3 CalculateForwardKinematicPosition()
    {
        Vector3 result = _joints[0].transform.position;
        Quaternion rotation = Quaternion.identity;
        for (int i = 1; i < _joints.Length; i++)
        {
            // rotation *= Quaternion.AngleAxis(_angles[i - 1], _joints[i - 1].RotationAxis);
            Vector3 axis = _joints[i].transform.localPosition;
            rotation *= _joints[i - 1].CalcRotation(_angles[i - 1]);
            result += rotation * Vector3.Scale(_joints[i].transform.localPosition, _armatureScale);
        }
        return result;
    }

    private float GetTargetApproximation()
    {
        Vector3 point = CalculateForwardKinematicPosition();
        return Vector3.Distance(point, _target.transform.position);
    }

    private void ApplyAngles()
    {
        for (int i = 0; i < _joints.Length - 1; i++)
        {
            _joints[i].Angle = _angles[i];
            // Vector3 axis = _joints[i + 1].transform.localPosition;
            // _joints[i].ApplyAngle(_angles[i], axis);
        }
    }

}
