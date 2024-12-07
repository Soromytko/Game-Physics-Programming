using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalAnimation : MonoBehaviour
{
    [SerializeField] private Transform _ikTarget;
    [SerializeField] private float _height = 0.3f;
    [SerializeField] private float _length = 0.5f;
    [SerializeField] private float _progress = 0.0f;
    private Vector3 _initialPosition;

    private void Awake()
    {
        _initialPosition = _ikTarget.position;
    }
    
    private void Update()
    {
        Animate(_progress, Time.deltaTime);
    }

    public void Animate(float progress, float delta)
    {
        // progress += progress > 1.0f ? 1.0f : progress < 0.0f ? + 1.0f : 0.0f;
        float angle = progress * Mathf.PI * 2;
        if (Mathf.Clamp(angle, 0, Mathf.PI) != angle) {
            return;
        }

        // Vector3 offset = Mathf.Clamp(angle, 0, Mathf.PI) == angle ? new Vector3(
        //     0,
        //     Mathf.Cos(angle) * _height,
        //     Mathf.Sin(angle) * _length
        // ) : Vector3.zero;

        Vector3 offset = new Vector3(0, Mathf.Sin(angle) * _height, Mathf.Cos(angle) * _length);

        _ikTarget.position = _initialPosition + offset;
    }
}
