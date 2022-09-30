using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float thrust;
    [SerializeField] float rotationForce;
    [SerializeField] AudioClip mainEngineThrustSound;
    [SerializeField] AudioClip sideBoosterSound;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;

    private AudioSource audioSource;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
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
            if (!isThrustSoundPlaying)
            {
                GameObject.Find("Engine").GetComponent<AudioSource>().Play();
                isThrustSoundPlaying = true;
            }
                

            if (!mainEngineParticles.isPlaying)
                mainEngineParticles.Play();

            rb.AddRelativeForce(Vector3.up * Time.deltaTime * thrust);
        }
        else
        {
            isThrustSoundPlaying = false;
            GameObject.Find("Engine").GetComponent<AudioSource>().Stop();
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
                GameObject.Find("Left Side Booster").GetComponent<AudioSource>().Play();
                isLeftSideBoosterSoundPlaying = true;
            }
                      
            rb.AddRelativeTorque(Vector3.forward * Time.deltaTime * rotationForce);
        }
        else
        {
            leftBoosterParticles.Stop();
            isLeftSideBoosterSoundPlaying = false;
            GameObject.Find("Left Side Booster").GetComponent<AudioSource>().Stop();
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (!rightBoosterParticles.isPlaying)
                rightBoosterParticles.Play();

            if (!isRightSideBoosterSoundPlaying)
            {
                GameObject.Find("Right Side Booster").GetComponent<AudioSource>().Play();
                isRightSideBoosterSoundPlaying = true;
            }

            rb.AddRelativeTorque(Vector3.forward * Time.deltaTime * -rotationForce);
        }
        else
        {
            rightBoosterParticles.Stop();
            isRightSideBoosterSoundPlaying = false;
            GameObject.Find("Right Side Booster").GetComponent<AudioSource>().Stop();    
        }
    }

    public void StopEngineParticles()
    {
        leftBoosterParticles.Stop();
        rightBoosterParticles.Stop();
        mainEngineParticles.Stop();
    }
}
