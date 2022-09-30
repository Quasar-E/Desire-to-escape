using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float thrust;
    [SerializeField] float rotationForce;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;

    private AudioSource mainEngineAudioSource;
    private AudioSource leftBoosterAudioSource;
    private AudioSource rightBoosterAudioSource;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        mainEngineAudioSource = GameObject.Find("Engine").GetComponent<AudioSource>();
        leftBoosterAudioSource = GameObject.Find("Left Side Booster").GetComponent<AudioSource>();
        rightBoosterAudioSource = GameObject.Find("Right Side Booster").GetComponent<AudioSource>();
    }

    void Update()
    {
        //check level loading
        if (GetComponent<CollisionHandler>().IsInTransition)
            return;

        ProcessThrust();
        ProcessRotation();
    }

    private bool isThrustSoundPlaying = false;
    private void ProcessThrust()
    { 
        if (Input.GetKey(KeyCode.Space))
        {
            //prevent multiple sound play
            if (!isThrustSoundPlaying)
            {
                mainEngineAudioSource.Play();
                isThrustSoundPlaying = true;
            }
                

            if (!mainEngineParticles.isPlaying)
                mainEngineParticles.Play();

            rb.AddRelativeForce(Vector3.up * Time.deltaTime * thrust);
        }
        else
        {
            isThrustSoundPlaying = false;
            mainEngineAudioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private bool isLeftSideBoosterSoundPlaying = false;
    private bool isRightSideBoosterSoundPlaying = false;
    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (!leftBoosterParticles.isPlaying)
                leftBoosterParticles.Play();

            if (!isLeftSideBoosterSoundPlaying)
            {
                leftBoosterAudioSource.Play();
                isLeftSideBoosterSoundPlaying = true;
            }
                      
            rb.AddRelativeTorque(Vector3.forward * Time.deltaTime * rotationForce);
        }
        else
        {
            leftBoosterParticles.Stop();
            isLeftSideBoosterSoundPlaying = false;
            leftBoosterAudioSource.Stop();
        }



        if (Input.GetKey(KeyCode.D))
        {
            if (!rightBoosterParticles.isPlaying)
                rightBoosterParticles.Play();

            if (!isRightSideBoosterSoundPlaying)
            {
                rightBoosterAudioSource.Play();
                isRightSideBoosterSoundPlaying = true;
            }

            rb.AddRelativeTorque(Vector3.forward * Time.deltaTime * -rotationForce);
        }
        else
        {
            rightBoosterParticles.Stop();
            isRightSideBoosterSoundPlaying = false;
            rightBoosterAudioSource.Stop();    
        }
    }

    public void StopEngineParticles()
    {
        leftBoosterParticles.Stop();
        rightBoosterParticles.Stop();
        mainEngineParticles.Stop();
    }
}
