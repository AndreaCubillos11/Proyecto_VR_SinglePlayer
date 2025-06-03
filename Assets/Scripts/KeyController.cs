using UnityEngine;
using TMPro; // Asegúrate de incluir esto

public class KeyController : MonoBehaviour
{
    public static KeyController Instance; // Instancia del singleton

    public int totalKey = 1; // Total de llaves
    private int collectedKeys = 0; // Llaves recolectadas
    public TMP_Text foldersText; // Texto para mostrar el conteo de llaves
    public GameObject door; // Puerta que se abre al completar
    public string UltimoMensaje = "Durante la misión del rover Perseverance en 2021, Dayana fue la primera persona en narrar una transmisión de la NASA completamente en español, acercando una misión espacial histórica a millones de hispanohablantes en todo el mundo.";

    void Awake()
    {
        Instance = this; // Configura el singleton
    }

    public void AddFolder() // Cambia el nombre a AddKey si es más apropiado
    {
        collectedKeys++; // Incrementa el conteo de llaves recolectadas
        if (foldersText != null) // Verifica que foldersText no sea null
        {
            foldersText.text = $"Busca el Logo de la NASA"; // Actualiza el texto
        }
        else
        {
            Debug.LogError("foldersText no está asignado en el Inspector.");
        }

        if (collectedKeys >= totalKey) // Verifica si se han recolectado todas las llaves
        {
            if (door != null) // Verifica que door no sea null
            {
                foldersText.text = UltimoMensaje;
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
