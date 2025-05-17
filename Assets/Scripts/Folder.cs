using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Folder : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 0.3f; // Small delay for visual/audio feedback

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
        // Notify the GameManager that the folder was collected
        GameManager.instance.AddFolder();

        // Optional: Visual/audio effect before destroying
        GetComponent<MeshRenderer>().enabled = false; // Make invisible
        GetComponent<Collider>().enabled = false; // Prevent double detection

        // Destroy after a small delay
        Destroy(gameObject, destroyDelay);
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
