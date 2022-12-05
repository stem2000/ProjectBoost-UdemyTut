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

    bool isTransitioning = false;


    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) { return;}

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
        isTransitioning = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(_crash);
        _crashParticles.Play();
        GetComponent<RocketMovement>().enabled = false;
        Invoke("ReloadLevel", _loadLevelDelay);
    }


    void StartSuccessSequence()
    {
        isTransitioning = true;
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
}
