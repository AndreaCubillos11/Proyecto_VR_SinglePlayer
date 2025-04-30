using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(MeshRenderer))]
public class Tile : MonoBehaviour
{
    [Header("Configuración")]
    public int tileValue; // Asignar 0-14 en el Inspector
    public TileContainer tileContainer;
    public float returnSpeed = 5f;

    [Header("Referencias")]
    [SerializeField] private Text numberText; // Opcional: texto flotante
    private Collider tileCollider;
    private MeshRenderer meshRenderer;
    private Vector3 originalPosition;
    private bool isDragging = false;

    private void Awake()
    {
        tileCollider = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        originalPosition = transform.position;
        UpdateNumberText();
    }

    private void UpdateNumberText()
    {
        if (numberText != null)
            numberText.text = tileValue.ToString();
    }

    private void OnMouseDown()
    {
        if (CanInteract()) StartDragging();
    }

    private bool CanInteract()
    {
        return Input.GetMouseButton(0) && !isDragging;
    }

    private void StartDragging()
    {
        isDragging = true;
        tileCollider.enabled = false;
        originalPosition = transform.position;
        
        if (tileContainer != null)
            tileContainer.UnregisterTile(this);
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                transform.position = hit.point + new Vector3(0, 0.5f, 0); // Offset para evitar clipping
            }
        }
    }

    private void OnMouseUp()
    {
        if (isDragging) StopDragging();
    }

    private void StopDragging()
    {
        isDragging = false;
        tileCollider.enabled = true;
        
        if (ShouldSnapToContainer())
            SnapToContainer();
        else
            StartCoroutine(SmoothReturn());
    }

    private bool ShouldSnapToContainer()
    {
        return tileContainer != null && 
               tileContainer.IsInDropZone(transform.position);
    }

    private void SnapToContainer()
    {
        Vector3 snapPosition = tileContainer.GetNearestSlotPosition(transform.position);
        transform.position = snapPosition;
        tileContainer.RegisterTile(this);
    }

    private IEnumerator SmoothReturn()
    {
        float elapsed = 0;
        Vector3 startPos = transform.position;
        
        while (elapsed < 1f)
        {
            transform.position = Vector3.Lerp(startPos, originalPosition, elapsed);
            elapsed += Time.deltaTime * returnSpeed;
            yield return null;
        }
        transform.position = originalPosition;
    }

    #if UNITY_EDITOR
    private void OnValidate()
    {
        if (numberText != null)
            numberText.text = tileValue.ToString();
    }
    #endif
}