using UnityEngine;
using UnityEngine.UI;

public class InstructionManager : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject instructionPanel;

    void Start()
    {
        Debug.Log("InstructionManager started");
        if (audioSource != null)
        {
            Debug.Log("AudioSource found, playing audio");
            audioSource.Play();
            Invoke("ShowInstructions", audioSource.clip.length);
        }
        else
        {
            Debug.LogError("AudioSource not assigned");
        }
    }

    void ShowInstructions()
    {
        Debug.Log("Attempting to show instructions");
        if (instructionPanel != null)
        {
            instructionPanel.SetActive(true);
            Debug.Log("Instructions shown");
            Invoke("HideInstructions", 15f);
        }
        else
        {
            Debug.LogError("Instruction panel not assigned");
        }
    }

    void HideInstructions()
    {
        Debug.Log("Hiding instructions");
        instructionPanel.SetActive(false);
    }
}
