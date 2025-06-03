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

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter detected with object: {other.gameObject.name}, tag: {other.tag}");
        Dep.text = $"Trigger Enter: {other.gameObject.name} (tag: {other.tag})";

        if (other.CompareTag("Player") && !isPlaying)
        {
            audioSource.Play();
            isPlaying = true;
            Debug.Log("Audio started playing.");
            Dep.text = $"Audio sonando - {other.gameObject.name}";
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log($"OnTriggerExit detected with object: {other.gameObject.name}, tag: {other.tag}");
        Dep.text = $"Trigger Exit: {other.gameObject.name} (tag: {other.tag})";

        if (other.CompareTag("Player") && isPlaying)
        {
            audioSource.Stop();
            isPlaying = false;
            Debug.Log("Audio stopped.");
            Dep.text = $"Audio detenido - {other.gameObject.name}";
        }
    }
}
