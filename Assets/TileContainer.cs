using UnityEngine;
using System.Collections.Generic;

public class TileContainer : MonoBehaviour
{
    [Header("Configuración del Puzzle")]
    public int[] correctOrder = {7, 3, 1, 9, 13, 2, 11, 4, 10, 6, 5, 8, 14, 12, 0};
    public Collider dropZone; // Collider 3D que define el área válida
    public DoorController doorController;

    private List<Tile> placedTiles = new List<Tile>();

    public void RegisterTile(Tile tile)
    {
        if (!placedTiles.Contains(tile))
        {
            placedTiles.Add(tile);
            CheckTileOrder();
        }
    }

    public void UnregisterTile(Tile tile)
    {
        if (placedTiles.Contains(tile))
        {
            placedTiles.Remove(tile);
            if (doorController.isOpen)
                doorController.CloseDoor();
        }
    }

    public Vector3 GetNearestSlotPosition(Vector3 position)
    {
        return new Vector3(
            Mathf.Round(position.x * 2) / 2,
            Mathf.Round(position.y * 2) / 2,
            Mathf.Round(position.z * 2) / 2
        );
    }

    public bool IsInDropZone(Vector3 position)
    {
        return dropZone != null && dropZone.bounds.Contains(position);
    }

    private void CheckTileOrder()
    {
        if (placedTiles.Count != correctOrder.Length)
            return;

        for (int i = 0; i < correctOrder.Length; i++)
        {
            if (placedTiles[i].tileValue != correctOrder[i])
            {
                Debug.Log($"Error en posición {i}: Esperado {correctOrder[i]}, Obtenido {placedTiles[i].tileValue}");
                return;
            }
        }
        doorController.OpenDoor();
    }
}