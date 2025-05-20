using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Number : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 0.3f; // Small delay for visual/audio feedback
    [SerializeField] private int numberValue; // The value of the number (1, 9, 8, or 3)

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Awake()
    {
        // Get the XRGrabInteractable component
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // Ensure the object has an XRGrabInteractable component
        if (grabInteractable == null)
        {
            grabInteractable = gameObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        }

        // Subscribe to the selectEntered event
        grabInteractable.selectEntered.AddListener(OnGrabbed);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Notify the NumberGameManager that the number was collected
        if (NumberGameManager.instance.CanCollectNumber(numberValue))
        {
            NumberGameManager.instance.CollectNumber(numberValue);

            // Optional: Visual/audio effect before destroying
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false; // Make invisible
            }

            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false; // Prevent double detection
            }

            // Destroy after a small delay
            Destroy(gameObject, destroyDelay);
        }
        else
        {
            Debug.Log("No es el número correcto en este momento. Debes seguir el orden: 1, 9, 8, 3.");
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from the event to prevent memory leaks
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        }
    }
}

