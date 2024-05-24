using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegProceduralAnimation : MonoBehaviour
{
    [SerializeField] private Transform _target;
    // [SerializeField] private Transform Pole;
    [SerializeField] private Transform _rayOrigin;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _stepDistance = 1f;
    [SerializeField] private float _stepHeight = 1f;
    [SerializeField] private Vector3 _oldPosition;
    [SerializeField] private Vector3 _currentPosition;
    [SerializeField] private Vector3 _newPosition;

    private float _lerp;

    private void Start()
    {
        _currentPosition = _newPosition = _oldPosition = _target.position;
    }

    private void Update()
    {
        _target.position = _currentPosition;

        if (UpdateNewPosition())
        {
            _lerp = 0f;
        }
        if (_lerp < 1f)
        {
            Vector3 footPosition = Vector3.Lerp(_oldPosition, _newPosition, _lerp);
            footPosition.y += Mathf.Sin(_lerp * Mathf.PI) * _stepHeight;
            _lerp = Mathf.MoveTowards(_lerp, 1f, Time.deltaTime *_speed);
            _currentPosition = footPosition;
        }
        else
        {
            _oldPosition = _newPosition;
        }
    }

    private bool UpdateNewPosition()
    {
        if (CastRayDown(_rayOrigin.position, out RaycastHit hit)) {
            Vector3 moveDirection = hit.point - _newPosition;
            float halfStepDistance = _stepDistance / 2f;
            if (moveDirection.magnitude > halfStepDistance)
            {
                if (CastRayDown(_rayOrigin.position + moveDirection.normalized * halfStepDistance, out hit)) {
                    _newPosition = hit.point;
                    return true;
                }
            }
        }
        return false;
    }

    private bool CastRayDown(Vector3 origin, out RaycastHit hit)
    {
        Ray ray = new Ray(origin, Vector3.down);
        if (Physics.Raycast(ray, out hit, 10)) {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_newPosition, 0.2f);
        Gizmos.DrawLine(_rayOrigin.position, _rayOrigin.position + Vector3.down * 10);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_oldPosition, 0.2f);
        // Gizmos.color = Color.green;
        // Gizmos.DrawSphere(_target.position, 0.2f);
    }
}

