using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float timeToReload;
    [SerializeField] float timeToLoadNext;
    [SerializeField] AudioClip successAudio;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    private AudioSource audioSource;

    public bool IsInTransition {get; private set;} = false;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();    
    }

    private void OnCollisionEnter(Collision other) 
    {
        //check if level load or DevKey are currently enabled
        if (IsInTransition || GetComponent<DevKeys>().IsCollisionDisabled)
            return;

        switch (other.gameObject.tag)
        {
            case "Friendly":
            break;

            case "Finish":
                ProcessSucess();
            break;

            default:
                ProcessCrash();
            break;
        }    
    }

    private void ProcessSucess()
    {
        IsInTransition = true;
        StopEngineSounds();
        audioSource.PlayOneShot(successAudio);
        successParticles.Play();

        GetComponent<Mover>().StopEngineParticles();

        Invoke("StartNextLevel", timeToLoadNext);
    }

    private void ProcessCrash()
    {
        IsInTransition = true;
        StopEngineSounds();
        audioSource.PlayOneShot(crashAudio);
        crashParticles.Play();

        GetComponent<Mover>().StopEngineParticles();

        Invoke("ReloadLevel", timeToReload);
    }

    public void StartNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex + 1 == SceneManager.sceneCountInBuildSettings)
            currentSceneIndex = -1;

        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void StopEngineSounds()
    {
        GameObject.Find("Engine").GetComponent<AudioSource>().Stop();
        GameObject.Find("Left Side Booster").GetComponent<AudioSource>().Stop();
        GameObject.Find("Right Side Booster").GetComponent<AudioSource>().Stop();
    }
}
