using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * _speed;
    }
}
