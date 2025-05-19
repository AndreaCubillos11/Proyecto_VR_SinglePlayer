using UnityEngine;

public class VRCodeInput : MonoBehaviour
{
    [Header("Referencia a la puerta")]
    public SlidingDoor slidingDoor; // Referencia al script que abre la puerta

    private string inputCode = ""; // Código que ingresa el usuario
    private const string correctCode = "1234"; // Código correcto

    // Método para agregar un dígito al código ingresado
    public void AddDigit(string digit)
    {
        if (inputCode.Length < 4)
        {
            inputCode += digit;
            Debug.Log("Código ingresado: " + inputCode);

            if (inputCode.Length == 4)
            {
                CheckCode();
            }
        }
    }

    // Verifica si el código ingresado es correcto
    private void CheckCode()
    {
        if (inputCode == correctCode)
        {
            Debug.Log("Código correcto. Abriendo puerta.");
            if (slidingDoor != null)
            {
                slidingDoor.OpenDoors();
            }
            else
            {
                Debug.LogError("¡Referencia a SlidingDoor no asignada!");
            }
        }
        else
        {
            Debug.LogWarning("Código incorrecto. Intenta de nuevo.");
        }

        inputCode = ""; // Resetea el código para nuevo intento
    }
}
