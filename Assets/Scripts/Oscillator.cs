using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 _startPosition;
    [SerializeField] Vector3 _movementDirection;
    [SerializeField] [Range(0,1)] float _movementFactor;
    [SerializeField] float _period = 2f;
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ObstacleOscillation();
    }

    void ObstacleOscillation()
    {
        float cycles = Time.time / _period;
        const float tau = Mathf.PI * 2;
        _movementFactor = (Mathf.Sin(tau * cycles) + 1) / 2;
        Vector3 offset = _movementDirection * _movementFactor;
        transform.position = _startPosition + offset;
    }
}
