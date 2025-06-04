using UnityEngine;

public class InstructionPanel : MonoBehaviour
{
    public GameObject instructionPanel;
    public float showDistance = 5f;
    public Transform target; // Asigna el transform del jugador en el inspector

    void Start()
    {
        instructionPanel.SetActive(false); // Asegúrate de que el panel esté desactivado al inicio
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < showDistance)
        {
            instructionPanel.SetActive(true);
            Invoke("HidePanel", 20f); // Oculta el panel después de 20 segundos
        }
    }

    void HidePanel()
    {
        instructionPanel.SetActive(false);
    }
}
