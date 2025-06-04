using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton

    public int totalFolders = 5;
    private int collectedFolders = 0;
    public TMP_Text foldersText;
    public GameObject door; // Puerta que se abre al completar
    public string MensajeFinal = "¿Sabías que cuando vivía en Cali, Colombia, de niña, Dayana guardaba recortes y fotos de cohetes y planetas en una carpeta escolar, soñando con trabajar algún día en la NASA? Años más tarde, ese sueño la llevó a convertirse en la primera mujer latinoamericana en liderar una misión de exploración en Marte.";

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
            foldersText.text = MensajeFinal;
            door.SetActive(false); // Abre la puerta
            Debug.Log("¡Todas las carpetas recolectadas!");
            
        }
    }
}