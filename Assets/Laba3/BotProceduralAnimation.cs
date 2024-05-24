using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotProceduralAnimation : MonoBehaviour
{
    [SerializeField] private float _animationProgress;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _stepHeight = 0.005f;
    [SerializeField] private float _armSwing = 0.005f;
    [SerializeField] private float _groundHeight = 0.003f;
    [SerializeField] private Transform _ground;
    [SerializeField] private IKData _leftLegData;
    [SerializeField] private IKData _rightLegData;
    [SerializeField] private IKData _leftHandData;
    [SerializeField] private IKData _rightHandData;
    public float Angle;

    private void Update()
    {
        _animationProgress += Time.deltaTime * _speed;
        if (_animationProgress >= 1f) _animationProgress -= 1f;
        
        AnimateLeg(_leftLegData, _animationProgress);
        AnimateLeg(_rightLegData, _animationProgress, 180f * Mathf.Deg2Rad);
        AnimateHand(_leftHandData, _animationProgress, 180f * Mathf.Deg2Rad);
        AnimateHand(_rightHandData, _animationProgress);
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
}

[System.Serializable]
public class IKData
{
    public Transform Target;
    public Transform Pole;
}
