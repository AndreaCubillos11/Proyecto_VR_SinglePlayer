using UnityEngine;

public class ActivateSoundOnProximityWithDistance : MonoBehaviour
{
    private AudioSource audioSource;
    public float maxDistance = 5.0f; // Distancia máxima para que el sonido se reproduzca
    private bool isPlaying = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is missing on the object.");
        }
        else
        {
            Debug.Log("AudioSource is configured correctly.");
        }
    }

    void Update()
    {
        if (audioSource != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance <= maxDistance && !isPlaying)
                {
                    audioSource.Play();
                    isPlaying = true;
                    Debug.Log("Audio should be playing.");
                }
                else if (distance > maxDistance && isPlaying)
                {
                    audioSource.Stop();
                    isPlaying = false;
                    Debug.Log("Audio should be stopped.");
                }
            }
        }
    }
}
