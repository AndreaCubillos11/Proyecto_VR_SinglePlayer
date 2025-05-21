using UnityEngine;
using TMPro; // Asegúrate de incluir esto

public class KeyController : MonoBehaviour
{
    public static KeyController Instance; // Instancia del singleton

    public int totalKey = 1; // Total de llaves
    private int collectedKeys = 0; // Llaves recolectadas
    public TMP_Text foldersText; // Texto para mostrar el conteo de llaves
    public GameObject door; // Puerta que se abre al completar

    void Awake()
    {
        Instance = this; // Configura el singleton
    }

    public void AddFolder() // Cambia el nombre a AddKey si es más apropiado
    {
        collectedKeys++; // Incrementa el conteo de llaves recolectadas
        if (foldersText != null) // Verifica que foldersText no sea null
        {
            foldersText.text = $"Carpetas: {collectedKeys}/{totalKey}"; // Actualiza el texto
        }
        else
        {
            Debug.LogError("foldersText no está asignado en el Inspector.");
        }

        if (collectedKeys >= totalKey) // Verifica si se han recolectado todas las llaves
        {
            if (door != null) // Verifica que door no sea null
            {
                door.SetActive(false); // Abre la puerta
            }
            else
            {
                Debug.LogError("door no está asignada en el Inspector.");
            }
            Debug.Log("¡Todas las carpetas recolectadas!");
        }
    }
}
