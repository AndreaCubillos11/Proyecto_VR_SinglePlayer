using UnityEngine;

public class Distance : MonoBehaviour
{
    public AudioSource AudioSource;

    void Start()
    {
        if (AudioSource == null)
        {
            Debug.LogError("AudioSource no asignado en el inspector.");
        }
    }

    void Update()
    {
        // Puedes añadir lógica de actualización aquí si es necesario
    }

    public void PlayAudio()
    {
        if (AudioSource != null)
        {
            AudioSource.Play();
            Debug.Log("Audio reproducido.");
        }
        else
        {
            Debug.LogError("AudioSource es null.");
        }
    }

    public void PauseAudio()
    {
        if (AudioSource != null)
        {
            AudioSource.Pause();
            Debug.Log("Audio pausado.");
        }
        else
        {
            Debug.LogError("AudioSource es null.");
        }
    }
}
