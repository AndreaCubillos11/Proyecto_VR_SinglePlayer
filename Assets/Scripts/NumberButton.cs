using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class NumberButton : MonoBehaviour
{
    [Header("Dígito que representa el botón")]
    public string digit;

    [Header("Referencia al script VRCodeInput del Keypad")]
    public VRCodeInput codeInput;

    // Método para ser llamado cuando se presiona el botón en VR
    public void PressButton()
    {
        if (codeInput != null)
        {
            Debug.Log("Presionando el botón con dígito: " + digit);
            codeInput.AddDigit(digit);
        }
        else
        {
            Debug.LogError("¡Referencia a VRCodeInput no asignada!");
        }
    }

    // Método para manejar la interacción con el controlador VR
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        PressButton();
    }
}
