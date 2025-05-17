using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;

    public int totalItems = 5;
    private int collectedItems = 0;

    // Referencia al script SlidingDoor para abrir la puerta cuando se recojan todos los objetos
    public SlidingDoor slidingDoor;

    // Puedes asignar la puerta completa (si quieres desactivarla)
    public GameObject door;

    void Awake()
    {
        // Singleton pattern para acceso fácil
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Este método se llama cuando se recoja un objeto con tag "Folder"
    public void ItemCollected()
    {
        collectedItems++;
        Debug.Log($"Objetos recogidos: {collectedItems}/{totalItems}");
        if (collectedItems >= totalItems)
        {
            Debug.Log("¡Todos los objetos recogidos! Puerta abierta.");

            // Opcional: desactivar la puerta física si usas un objeto que bloquea el paso
            if (door != null)
            {
                door.SetActive(false);
            }

            // Abrimos las puertas deslizantes
            if (slidingDoor != null)
            {
                slidingDoor.OpenDoors();
            }
        }
    }

    // Detectar trigger con objetos "Folder" desde el rig VR o los controladores
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Folder"))
        {
            ItemCollected();
            Destroy(other.gameObject); // Elimina el ítem recogido
        }
    }
}

