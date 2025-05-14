using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class TileVRController : MonoBehaviour
{
    private Tile tileScript;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private TileContainer tileContainer;
    private Vector3 originalPosition;
    private bool wasGrabbed = false;
    private GridLayoutHelper gridHelper;

    private void Awake()
    {
        // Obtener los componentes necesarios
        tileScript = GetComponent<Tile>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        
        if (tileScript != null)
        {
            tileContainer = tileScript.tileContainer;
        }
        
        // Buscar el helper de cuadrícula
        gridHelper = FindObjectOfType<GridLayoutHelper>();
        
        // Configurar eventos del interactable
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Guardar posición original y desregistrar del contenedor
        originalPosition = transform.position;
        wasGrabbed = true;
        
        if (tileContainer != null)
        {
            tileContainer.UnregisterTile(tileScript);
        }
        
        Debug.Log($"Ficha VR {tileScript.tileValue} agarrada.");
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (!wasGrabbed) return;
        wasGrabbed = false;
        
        // Verificar si se soltó en la zona válida
        if (tileContainer != null && tileContainer.IsInDropZone(transform.position))
        {
            // Ajustar a la posición más cercana en la cuadrícula
            Vector3 snapPosition;
            
            // Usar el helper de cuadrícula si está disponible
            if (gridHelper != null)
            {
                snapPosition = gridHelper.GetNearestGridPosition(transform.position);
            }
            else
            {
                snapPosition = tileContainer.GetNearestSlotPosition(transform.position);
            }
            
            transform.position = snapPosition;
            tileContainer.RegisterTile(tileScript);
            
            Debug.Log($"Ficha VR {tileScript.tileValue} colocada en la posición {snapPosition}.");
        }
        else
        {
            // Devolver a la posición original
            StartCoroutine(SmoothReturnCoroutine());
        }
    }

    private System.Collections.IEnumerator SmoothReturnCoroutine()
    {
        float elapsed = 0;
        Vector3 startPos = transform.position;
        float returnSpeed = tileScript.returnSpeed;

        while (elapsed < 1f)
        {
            transform.position = Vector3.Lerp(startPos, originalPosition, elapsed);
            elapsed += Time.deltaTime * returnSpeed;
            yield return null;
        }
        
        transform.position = originalPosition;
    }
}