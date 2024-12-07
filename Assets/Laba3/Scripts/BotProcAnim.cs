using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotProcAnim : MonoBehaviour
{
    [SerializeField] private Transform _origin;
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private float _stepHeight = 2.0f;
    [SerializeField] private float _stepDistance = 1.0f;
    private Vector3 _oldPosition;
    private Vector3 _currentPosition;
    private Vector3 _newPosition;
    private float _lerp;

    private void Start()
    {
        _currentPosition = transform.position;
        _newPosition = _currentPosition;
    }

    private void Update()
    {
        transform.position = _currentPosition;

        if (Physics.Raycast(_origin.position, Vector3.down, out RaycastHit hit, Mathf.Infinity))
        {
            if (Vector3.Distance(_newPosition, hit.point) > _stepDistance)
            {
                _lerp = 0.0f;
                _newPosition = hit.point;
            }
        }

        if (_lerp < 1)
        {
            Vector3 footPosition = Vector3.Lerp(_oldPosition, _newPosition, _lerp);
            footPosition.y += Mathf.Sin(_lerp * Mathf.PI) * _stepHeight;

            _currentPosition = footPosition;
            _lerp += Time.deltaTime * _speed;
        }
        else
        {
            _oldPosition = _newPosition;
        }


    }

    private void OnDrawGizmos()
    {
        return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_newPosition, 0.5f);
    }
}
