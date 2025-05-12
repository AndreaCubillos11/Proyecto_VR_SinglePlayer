using UnityEngine;
using System.Collections.Generic;

public class TileContainer : MonoBehaviour
{
    [Header("Configuración del Puzzle")]
    public int[] correctOrder = {7, 3, 1, 9, 13, 2, 11, 4, 10, 6, 5, 8, 14, 12, 0};
    public Collider dropZone; // Collider que define el área válida
    public DoorController doorController;

    // Delegado para permitir estrategias personalizadas de posicionamiento
    public System.Func<Vector3, Vector3> GetNearestSlotPositionDelegate;

    private List<Tile> placedTiles = new List<Tile>();

    private void Start()
    {
        // Intentar encontrar un GridLayoutHelper si existe
        GridLayoutHelper gridHelper = FindObjectOfType<GridLayoutHelper>();
        if (gridHelper != null)
        {
            GetNearestSlotPositionDelegate = gridHelper.GetNearestGridPosition;
        }
    }

    public void RegisterTile(Tile tile)
    {
        if (!placedTiles.Contains(tile))
        {
            placedTiles.Add(tile);
            Debug.Log($"Ficha {tile.tileValue} registrada en el contenedor.");
            CheckTileOrder();
        }
    }

    public void UnregisterTile(Tile tile)
    {
        if (placedTiles.Contains(tile))
        {
            placedTiles.Remove(tile);
            Debug.Log($"Ficha {tile.tileValue} desregistrada del contenedor.");
            if (doorController != null && doorController.isOpen)
                doorController.CloseDoor();
        }
    }

    public Vector3 GetNearestSlotPosition(Vector3 position)
    {
        // Si hay un delegado personalizado, usarlo
        if (GetNearestSlotPositionDelegate != null)
        {
            return GetNearestSlotPositionDelegate(position);
        }

        // Implementación por defecto (cuadrícula simple)
        return new Vector3(
            Mathf.Round(position.x * 2) / 2,
            Mathf.Round(position.y * 2) / 2,
            Mathf.Round(position.z * 2) / 2
        );
    }

    public bool IsInDropZone(Vector3 position)
    {
        if (dropZone == null) return false;
        
        // Para BoxCollider, hacemos una proyección en XZ
        if (dropZone is BoxCollider)
        {
            Bounds bounds = dropZone.bounds;
            // Comprobar si la posición X,Z está dentro, ignorando Y
            return position.x >= bounds.min.x && position.x <= bounds.max.x &&
                   position.z >= bounds.min.z && position.z <= bounds.max.z;
        }
        
        // Para otros tipos de Collider
        return dropZone.bounds.Contains(position);
    }

    private void CheckTileOrder()
    {
        if (placedTiles.Count != correctOrder.Length)
        {
            Debug.Log($"Número de fichas colocadas: {placedTiles.Count}, se esperaban: {correctOrder.Length}");
            return;
        }

        for (int i = 0; i < correctOrder.Length; i++)
        {
            if (placedTiles[i].tileValue != correctOrder[i])
            {
                Debug.Log($"Error en posición {i}: Esperado {correctOrder[i]}, Obtenido {placedTiles[i].tileValue}");
                return;
            }
        }

        Debug.Log("¡Orden correcto! Abriendo la puerta.");
        if (doorController != null)
            doorController.OpenDoor();
    }
    
    // Método de depuración para imprimir el estado actual
    public void PrintCurrentState()
    {
        string state = "Estado actual: ";
        foreach (Tile tile in placedTiles)
        {
            state += tile.tileValue + ", ";
        }
        Debug.Log(state);
    }

    // Añade este método al TileContainer
public bool TryPlaceTile(Tile tile, Vector3 dropPosition)
{
    // Verifica si está en la zona de colocación
    if (!IsInDropZone(dropPosition)) return false;

    // Obtiene la posición más cercana usando el GridLayoutHelper
    Vector3 nearestPosition = GetNearestSlotPosition(dropPosition);
    
    // Verifica si la posición es válida (no demasiado lejos del punto de colocación)
    if (Vector3.Distance(dropPosition, nearestPosition) > 1.0f) return false;

    // Coloca la ficha en la posición calculada
    tile.transform.position = nearestPosition;
    RegisterTile(tile);
    return true;
}
}