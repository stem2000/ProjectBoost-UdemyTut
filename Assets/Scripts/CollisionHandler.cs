using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float _loadLevelDelay = 2f;
    [SerializeField] AudioClip _success;
    [SerializeField] AudioClip _crash;

    [SerializeField] ParticleSystem _successParticles;
    [SerializeField] ParticleSystem _crashParticles;

    AudioSource _audioSource;

    bool _isTransitioning = false;
    bool _isCollisionsDisabled = false;

    [SerializeField] int _xMaxPos = 100;
    [SerializeField] int _yMaxPos = 100;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        RespondToDebugKeys();
        ControlPlayerPosition();
    }


    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            _isCollisionsDisabled = !_isCollisionsDisabled;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_isTransitioning || _isCollisionsDisabled) { return;}

        switch (collision.gameObject.tag)
        {
            case "Start":
                Debug.Log("Start");
                break;
            case "Finish":
                StartSuccessSequence();
                break; 
            case "Friendly":
                Debug.Log("Friendly");
                break;
            default:
                StartCrashSequence();
                break;
        }    
    }


    void StartCrashSequence()
    {
        _isTransitioning = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(_crash);
        _crashParticles.Play();
        GetComponent<RocketMovement>().enabled = false;
        Invoke("ReloadLevel", _loadLevelDelay);
    }


    void StartSuccessSequence()
    {
        _isTransitioning = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(_success);
        _successParticles.Play();
        GetComponent<RocketMovement>().enabled = false;
        Invoke("LoadNextLevel", _loadLevelDelay);
    }


    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }


    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }


    void ControlPlayerPosition()
    {
        if((Mathf.Abs(transform.position.x) > _xMaxPos || Mathf.Abs(transform.position.y) > _yMaxPos) && !_isTransitioning)
        {
            _isTransitioning = true;
            StartCrashSequence();
        } 
    }
}
