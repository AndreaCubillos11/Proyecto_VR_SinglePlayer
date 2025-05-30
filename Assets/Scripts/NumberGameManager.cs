using UnityEngine;
using TMPro; // Asegúrate de incluir este espacio de nombres para usar TextMeshPro

public class NumberGameManager : MonoBehaviour
{
    public static NumberGameManager instance; // Singleton instance

    public int[] correctSequence = { 1, 9, 8, 3 };
    private int currentIndex = 0;
    public TMP_Text collectedNumbersText; // Usa TMP_Text para TextMeshPro
    public GameObject[] doors; // Array para manejar múltiples puertas
    public string finalMessage = "¡Felicidades! Has encontrado el año de nacimiento de Dayana: 1983.";

    // Referencia al script SlidingDoor para abrir las puertas deslizantes
    public SlidingDoor[] slidingDoors;

    // Nueva referencia al segundo panel
    public GameObject secondPanel; // Asegúrate de asignar este panel en el Inspector

    void Awake()
    {
        // Singleton pattern implementation
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public bool CanCollectNumber(int number)
    {
        return number == correctSequence[currentIndex];
    }

    public void CollectNumber(int number)
    {
        currentIndex++;
        collectedNumbersText.text += number.ToString();

        if (currentIndex == correctSequence.Length)
        {
            collectedNumbersText.text = finalMessage;
            OpenDoors(); // Abre las puertas
        }
    }

    void OpenDoors()
    {
        // Desactivar las puertas físicas si es necesario
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
        }

        // Abrir las puertas deslizantes
        foreach (SlidingDoor slidingDoor in slidingDoors)
        {
            if (slidingDoor != null)
            {
                slidingDoor.OpenDoors();
            }
        }

        // Activar el segundo panel
        if (secondPanel != null)
        {
            secondPanel.SetActive(true);
            Debug.Log("Segundo panel activado.");
        }
        else
        {
            Debug.LogWarning("El segundo panel no está asignado en el Inspector.");
        }
    }
}
