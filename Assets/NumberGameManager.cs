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
        foreach (GameObject door in doors)
        {
            door.SetActive(false); // Abre cada puerta
        }
    }
}
