using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 5f; // Distancia máxima de interacción
    public LayerMask buttonLayer; // Capa en la que están los botones
    private Button currentButton; // Botón actualmente señalado

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Definir el rayo aquí
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.O)) // Detecta la tecla "O"
        {
            if (Physics.Raycast(ray, out hit, interactionDistance, buttonLayer))
            {
                Button button = hit.collider.GetComponent<Button>();
                if (button != null)
                {
                    // Ejecutar la acción del botón
                    button.onClick.Invoke();
                }
            }
        }

        // Resaltar el botón si está en el rango de interacción
        RaycastHit highlightHit;
        if (Physics.Raycast(ray, out highlightHit, interactionDistance, buttonLayer))
        {
            Button button = highlightHit.collider.GetComponent<Button>();
            if (button != null && button != currentButton)
            {
                if (currentButton != null)
                {
                    // Restablecer el color del botón anterior
                    ColorBlock colors = currentButton.colors;
                    colors.normalColor = Color.white; // Color original
                    currentButton.colors = colors;
                }

                // Resaltar el nuevo botón
                currentButton = button;
                ColorBlock highlightColors = currentButton.colors;
                highlightColors.normalColor = Color.yellow; // Color de resaltado
                currentButton.colors = highlightColors;
            }
        }
        else if (currentButton != null)
        {
            // Restablecer el color si no hay botón señalado
            ColorBlock colors = currentButton.colors;
            colors.normalColor = Color.white; // Color original
            currentButton.colors = colors;
            currentButton = null;
        }
    }
}
