using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegProceduralAnimation : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _stepDistance = 1f;
    // [SerializeField] private Transform Pole;
    [SerializeField] private Vector3 _oldPosition
    [SerializeField] private Vector3 _newPosition;

    private float _lerp;
    public float Angle;

    private void Start()
    {
        _oldPosition = transform.position - Vector3.forward * _stepDistance;
        _newPosition = transform.position + Vector3.forward * _stepDistance;
    }

    private void Update()
    {
        UpdateNewPosition();
        float animationProgress = CalculateAnimationProgress();
        Animate(animationProgress);        
    }

    private float CalculateAnimationProgress()
    {
        Vector3 direction = _newPosition - _oldPosition;
        Vector3 targetDirection = _target.position - _oldPosition;
        Vector3 project = Vector3.Project(targetDirection, direction);
        return direction.Length == 0f ? 1f : project.Length / direction.Length;
    }

    private bool UpdateNewPosition()
    {
        Ray ray = new Ray(_rayOrigin.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 10))
        {
            Vector3 hitDirection = hit.point - _newPosition;
            float hitDistance = hitDirection.Length;
            if (hitDistance > _stepDistance)
            {
                _oldPosition = _newPosition;
                _newPosition += hitDirection.normalized * (int)(hitDistance / _stepDistance);
                return true;
            }
        }
        return false
    }

    private void Animate(float progress)
    {
        _target.position = Vector3.MoveTowards(_target.position, _newPosition, delta);
    }

    private void AnimateLeg(IKData leg, float progress, float angleOffset = 0f)
    {
        float angle = progress * 2 * Mathf.PI;
        leg.Target.localPosition = new Vector3(
            Mathf.Cos(angle + angleOffset) * _stepHeight,
            leg.Target.localPosition.y,
            Mathf.Sin(angle + angleOffset) * _stepHeight
        );

        if (leg.Target.position.y < _ground.position.y) {
            leg.Target.position = new Vector3(leg.Target.position.x, _ground.position.y, leg.Target.position.z);
        }
    }

    private void AnimateHand(IKData hand, float progress, float angleOffset = 0f)
    {
        float angle = progress * 2 * Mathf.PI;
        hand.Target.localPosition = new Vector3(
            Mathf.Cos(angle + angleOffset) * _armSwing,
            // Mathf.Sin(angle * (progress > 0.5 ? +1 : -1) + angleOffset) * _armSwing,
            (Mathf.Cos((angle + angleOffset) * 2) - 1.0f) / 2 * _armSwing,
            hand.Target.localPosition.z
        ) + Vector3.up * 0.004f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DriweSphere(_newPosition, 0.5f);
        Gizmos.color = Color.yellow;
        Gizmos.DriweSphere(_oldPosition, 0.5f);
    }
}

