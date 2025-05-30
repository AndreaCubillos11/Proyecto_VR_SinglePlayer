using UnityEngine;
using TMPro;

public class Distancia : MonoBehaviour
{
    private AudioSource audioSource;
    public float maxDistance = 5.0f; // Distancia máxima para que el sonido se reproduzca
    private bool isPlaying = false;
    public TMP_Text Dep;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is missing on the object.");
            Dep.text = "No hay audio";
        }
        else
        {
            Debug.Log("AudioSource is configured correctly.");
            Dep.text = "Si hay audio";
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
               Dep.text = "Distancia: " + distance.ToString();
                if (distance <= maxDistance && !isPlaying)
                {
                    audioSource.Play();
                    isPlaying = true;
                    Debug.Log("Audio should be playing.");
                    Dep.text = "Audio sonando" + distance.ToString();
                }
                else if (distance > maxDistance && isPlaying)
                {
                    audioSource.Stop();
                    isPlaying = false;
                    Debug.Log("Audio should be stopped.");
                    Dep.text = "Audio detenido" + distance.ToString();
                }else
                {
                    //Dep.text = "Distancia: "
                }
            }
        }
    }
}
