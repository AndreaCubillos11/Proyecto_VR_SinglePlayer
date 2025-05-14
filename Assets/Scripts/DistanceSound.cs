using UnityEngine;
using UnityEngine.UI;

public class DistanceSoundManager : MonoBehaviour
{
    public AudioSource audioClip;
    public GameObject player;
    public Canvas canvas; // Contiene los botones de reproducir/pausar
    public float activationDistance = 5f;

    void Start()
    {
        if (canvas != null)
            canvas.enabled = false;

        // Asegúrate de que el audio no se reproduzca al inicio
        if (audioClip != null)
            audioClip.Stop();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= activationDistance)
        {
            if (canvas != null) canvas.enabled = true;
        }
        else
        {
            if (audioClip.isPlaying) audioClip.Stop();
            if (canvas != null) canvas.enabled = false;
        }
    }

    // Método público para ser llamado desde el botón Play
    public void StartAudio()
    {
        if (audioClip != null && !audioClip.isPlaying)
            audioClip.Play();
    }

    // Método público para ser llamado desde el botón Pause
    public void StopAudio()
    {
        if (audioClip != null && audioClip.isPlaying)
            audioClip.Pause();
    }
}
