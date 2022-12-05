using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] float _thrustForce = 100f;
    [SerializeField] float _rotationSpeed = 10f;
    [SerializeField] AudioClip _mainThrust;
    [SerializeField] ParticleSystem _mainThruster;
    [SerializeField] ParticleSystem _rightThruster;
    [SerializeField] ParticleSystem _leftThruster;
    Rigidbody _rocketRb;
    AudioSource _audioSource;

    bool _isRotating = false;

    void Start()
    {
        _rocketRb = GetComponent<Rigidbody>();        
        _audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }


    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }


    private void StopThrusting()
    {
        if (_audioSource.isPlaying && !_isRotating)
        {
            _audioSource.Stop();
        }
        _mainThruster.Stop();
    }


    void StartThrusting()
    {
        _rocketRb.AddRelativeForce(Vector3.up * _thrustForce * Time.deltaTime);
        if (!_audioSource.isPlaying)
            _audioSource.PlayOneShot(_mainThrust);
        if (!_mainThruster.isPlaying)
            _mainThruster.Play();
    }


    void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {            
            RotateRight();
        }
        else
        {
            StopSidesThrusting();
        }
    }


    private void RotateRight()
    {
        _isRotating = true;
        AddRotation(-_rotationSpeed);
        if (!_audioSource.isPlaying)
            _audioSource.PlayOneShot(_mainThrust);
        if (!_leftThruster.isPlaying)
            _leftThruster.Play();
    }


    private void RotateLeft()
    {
        _isRotating = true;
        AddRotation(_rotationSpeed);
        if (!_audioSource.isPlaying)
            _audioSource.PlayOneShot(_mainThrust);
        if (!_rightThruster.isPlaying)
            _rightThruster.Play();
    }


    private void StopSidesThrusting()
    {
        _isRotating = false;
        _rightThruster.Stop();
        _leftThruster.Stop();
    }


    void AddRotation(float rotationPerFrame)
    {
        _rocketRb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationPerFrame * Time.deltaTime);
        _rocketRb.freezeRotation = false;
    }

}
