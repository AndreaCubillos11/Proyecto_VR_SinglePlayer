using UnityEngine;

public class Folder : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 0.3f; // Pequeño delay para feedback visual/sonoro

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Si el jugador la toca
        {
            // Notifica al GameManager que se recolectó
            GameManager.instance.AddFolder(); 

            // Opcional: Efecto visual/sonoro antes de destruir
            GetComponent<MeshRenderer>().enabled = false; // Hace invisible
            GetComponent<Collider>().enabled = false;     // Evita doble detección

            Destroy(gameObject, destroyDelay); // Destruye tras un pequeño delay
        }
    }
}