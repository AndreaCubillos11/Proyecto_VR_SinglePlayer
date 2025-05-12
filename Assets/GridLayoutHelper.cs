using UnityEngine;
using System.Collections.Generic;

public class GridLayoutHelper : MonoBehaviour
{
    [Header("Referencias")]
    public TileContainer tileContainer;
    public Collider boardCollider;
    
    [Header("Configuración de Cuadrícula")]
    public int gridColumns = 5;
    public int gridRows = 3;
    public float heightOffset = 0.05f;
    
    private Dictionary<int, Vector2Int> tilePositions = new Dictionary<int, Vector2Int>();
    private Dictionary<Vector2Int, bool> occupiedSlots = new Dictionary<Vector2Int, bool>();

    private void Start()
    {
        if (boardCollider == null && tileContainer != null)
        {
            boardCollider = tileContainer.dropZone;
        }

        InitializeTilePositions();
        InitializeOccupiedSlots();
        
        // Registrar este helper en el TileContainer
        if (tileContainer != null)
        {
            tileContainer.GetNearestSlotPositionDelegate = GetNearestGridPosition;
        }
    }

    // Asigna a cada valor de ficha una posición en la cuadrícula según el orden correcto
    private void InitializeTilePositions()
    {
        if (tileContainer == null) return;

        for (int i = 0; i < tileContainer.correctOrder.Length; i++)
        {
            int row = i / gridColumns;
            int col = i % gridColumns;
            tilePositions[tileContainer.correctOrder[i]] = new Vector2Int(col, row);
        }
    }

    private void InitializeOccupiedSlots()
    {
        occupiedSlots.Clear();
        for (int row = 0; row < gridRows; row++)
        {
            for (int col = 0; col < gridColumns; col++)
            {
                occupiedSlots[new Vector2Int(col, row)] = false;
            }
        }
    }

    // Obtiene la posición correcta para una ficha basada en su valor
    public Vector3 GetNearestGridPosition(Vector3 inputPosition)
    {
        // Primero verifica si hay una ficha cerca que podamos estar moviendo
        Collider[] nearbyColliders = Physics.OverlapSphere(inputPosition, 0.5f);
        foreach (Collider col in nearbyColliders)
        {
            Tile tile = col.GetComponent<Tile>();
            if (tile != null && tilePositions.ContainsKey(tile.tileValue))
            {
                Vector2Int gridPos = tilePositions[tile.tileValue];
                if (!occupiedSlots[gridPos])
                {
                    return GetGridCellPosition(gridPos.y, gridPos.x);
                }
            }
        }
        
        // Si no encontramos una ficha válida, devolvemos la posición de entrada
        return inputPosition;
    }

    // Calcula la posición mundial de una celda de la cuadrícula
    public Vector3 GetGridCellPosition(int row, int col)
    {
        if (boardCollider == null) return Vector3.zero;
        
        Bounds bounds = boardCollider.bounds;
        float cellWidth = bounds.size.x / gridColumns;
        float cellHeight = bounds.size.z / gridRows;
        
        Vector3 topLeft = new Vector3(
            bounds.min.x + cellWidth / 2,
            bounds.max.y + heightOffset,
            bounds.max.z - cellHeight / 2
        );
        
        return topLeft + new Vector3(
            col * cellWidth,
            0,
            -row * cellHeight
        );
    }

    // Método llamado cuando una ficha es colocada en la cuadrícula
    public void OnTilePlaced(Tile tile)
    {
        if (tilePositions.ContainsKey(tile.tileValue))
        {
            Vector2Int gridPos = tilePositions[tile.tileValue];
            occupiedSlots[gridPos] = true;
            tileContainer.RegisterTile(tile);
        }
    }

    // Método llamado cuando una ficha es removida de la cuadrícula
    public void OnTileRemoved(Tile tile)
    {
        if (tilePositions.ContainsKey(tile.tileValue))
        {
            Vector2Int gridPos = tilePositions[tile.tileValue];
            occupiedSlots[gridPos] = false;
            tileContainer.UnregisterTile(tile);
        }
    }

    // Dibuja la cuadrícula en el editor
    private void OnDrawGizmos()
    {
        if (boardCollider == null) return;
        
        Bounds bounds = boardCollider.bounds;
        float cellWidth = bounds.size.x / gridColumns;
        float cellHeight = bounds.size.z / gridRows;
        
        Gizmos.color = Color.white;
        
        // Dibuja líneas horizontales
        for (int row = 0; row <= gridRows; row++)
        {
            float z = bounds.max.z - row * cellHeight;
            Gizmos.DrawLine(
                new Vector3(bounds.min.x, bounds.max.y + 0.01f, z),
                new Vector3(bounds.max.x, bounds.max.y + 0.01f, z)
            );
        }
        
        // Dibuja líneas verticales
        for (int col = 0; col <= gridColumns; col++)
        {
            float x = bounds.min.x + col * cellWidth;
            Gizmos.DrawLine(
                new Vector3(x, bounds.max.y + 0.01f, bounds.min.z),
                new Vector3(x, bounds.max.y + 0.01f, bounds.max.z)
            );
        }
        
        // Dibuja las posiciones de las fichas correctas
        if (tileContainer != null && tileContainer.correctOrder != null)
        {
            for (int i = 0; i < tileContainer.correctOrder.Length; i++)
            {
                int row = i / gridColumns;
                int col = i % gridColumns;
                Vector3 pos = GetGridCellPosition(row, col);
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(pos, new Vector3(cellWidth * 0.8f, 0.1f, cellHeight * 0.8f));
                
                // Mostrar el número esperado
                UnityEditor.Handles.Label(pos, tileContainer.correctOrder[i].ToString());
            }
        }
    }
}