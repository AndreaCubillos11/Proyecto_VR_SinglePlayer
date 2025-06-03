using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton

    public int totalFolders = 5;
    private int collectedFolders = 0;
    public TMP_Text foldersText;
    public GameObject door; // Puerta que se abre al completar
    public string MensajeFinal = "Sabias que cuando Dayana era niña en Cali, Colombia, usaba una carpeta escolar para guardar recortes y fotos de cohetes y planetas, soñando con trabajar algún día en la NASA. Años después, se convirtió en la primera mujer latinoamericana en liderar una misión de exploración en Marte.";

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