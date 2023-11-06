using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InverseIinematics : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _FKTarget;
    [SerializeField] private Joint[] _joints;

    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private Vector3 _forwardKinematicPosition;

    private void Start()
    {
        // Apply some angles for an example
        _joints[0].transform.localEulerAngles = new Vector3(-27f, 0, 0);
        _joints[1].transform.localEulerAngles = new Vector3(-18f, 0, 0);
        _joints[2].transform.localEulerAngles = new Vector3(25f, 0, 0);
        _joints[3].transform.localEulerAngles = new Vector3(-53f, 0, 0);
        _joints[4].transform.localEulerAngles = new Vector3(0f, 0, 0);
    }

    private void Update()
    {
        _targetPosition = _target.position;

        _forwardKinematicPosition = getForwardKinematicPosition(new float[]
        {
           _joints[0].transform.localEulerAngles.x,
           _joints[1].transform.localEulerAngles.x,
           _joints[2].transform.localEulerAngles.x,
           _joints[3].transform.localEulerAngles.x,
           _joints[4].transform.localEulerAngles.x,
        });

        _FKTarget.position = _forwardKinematicPosition;
    }

    private Vector3 getForwardKinematicPosition(float[] angles)
    {
        Vector3 result = _joints[0].transform.position;
        Quaternion rotation = Quaternion.identity;
        for (int i = 1; i < _joints.Length; i++)
        {
            rotation *= Quaternion.AngleAxis(angles[i - 1], _joints[i - 1].RotationAxis);
            result += rotation * _joints[i].transform.localPosition;
        }
        return result;
    }
}
