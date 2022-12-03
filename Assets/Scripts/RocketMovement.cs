using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] float _thrustForce = 100f;
    [SerializeField] float _rotationSpeed = 10f;
    Rigidbody _rocketRb;


    void Start()
    {
        _rocketRb = GetComponent<Rigidbody>();        
    }

    
    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }


    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space)){
            _rocketRb.AddRelativeForce(Vector3.up * _thrustForce * Time.deltaTime);
        }
    }


    void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            AddRotation(_rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            AddRotation(-_rotationSpeed);
        }
    }


    void AddRotation(float rotationPerFrame)
    {
        _rocketRb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationPerFrame * Time.deltaTime);
        _rocketRb.freezeRotation = false;
    }
}
