using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton

    public int totalFolders = 5;
    private int collectedFolders = 0;
    public TMP_Text foldersText;
    public GameObject door; // Puerta que se abre al completar

    void Awake()
    {
        instance = this; // Configura el singleton
    }

    public void AddFolder()
    {
        collectedFolders++;
        foldersText.text = $"Carpetas: {collectedFolders}/{totalFolders}";

        if (collectedFolders >= totalFolders)
        {
            door.SetActive(false); // Abre la puerta
            Debug.Log("¡Todas las carpetas recolectadas!");
        }
    }
}