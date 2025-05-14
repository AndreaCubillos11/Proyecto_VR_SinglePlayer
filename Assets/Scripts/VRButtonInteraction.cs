using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class VRButtonInteraction : MonoBehaviour
{
    private Button button;
    public Distance distanceScript; // Asigna este campo en el inspector

    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button component not found.");
        }
    }

    void Update()
    {
        // Verificar interacciones con el controlador izquierdo
        CheckControllerGrip(XRNode.LeftHand);

        // Verificar interacciones con el controlador derecho
        CheckControllerGrip(XRNode.RightHand);
    }

    void CheckControllerGrip(XRNode controllerNode)
    {
        // Obtener el dispositivo de entrada del controlador
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);

        // Verificar si el dispositivo es válido
        if (device.isValid)
        {
            // Detectar la interacción con el botón de agarrar del controlador
            if (device.TryGetFeatureValue(CommonUsages.gripButton, out bool gripValue) && gripValue)
            {
                // Simular un clic en el botón cuando se presiona el botón de agarrar del controlador
                if (button != null)
                {
                    button.onClick.Invoke();
                }
                else
                {
                    Debug.LogError("Button is null.");
                }
            }
        }
    }

    void OnButtonClick()
    {
        // Realizar una acción cuando se hace clic en el botón
        Debug.Log("Botón de la UI clickeado: " + gameObject.name);

        if (distanceScript != null)
        {
            distanceScript.PlayAudio();
        }
        else
        {
            Debug.LogError("Distance script not assigned.");
        }
    }
}
