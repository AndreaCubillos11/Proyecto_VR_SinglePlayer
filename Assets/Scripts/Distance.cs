using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : MonoBehaviour
{
    private bool pressState = false;
    private Animator animator;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void TogglePressState()
    {
        pressState = !pressState;
        animator.SetBool("Pressed", pressState);
        PlayAudio();
    }

    private void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Stop();  // Detener si está sonando
            audioSource.Play();  // Reproducir el audio
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        TogglePressState();
    }

    private void OnTriggerExit(Collider col)
    {
        TogglePressState();
    }
}
