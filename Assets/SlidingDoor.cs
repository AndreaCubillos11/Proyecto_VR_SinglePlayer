using System.Collections;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    [Header("Left Door Settings")]
    public Transform leftDoor;
    public Vector3 leftOpenPosition;
    public Vector3 leftClosedPosition;

    [Header("Right Door Settings")]
    public Transform rightDoor;
    public Vector3 rightOpenPosition;
    public Vector3 rightClosedPosition;

    [Header("General Settings")]
    public float doorSpeed = 2f;

    private bool isOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            OpenDoors();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isOpen)
        {
            CloseDoors();
        }
    }

    private void OpenDoors()
    {
        StopAllCoroutines();
        StartCoroutine(MoveDoor(leftDoor, leftOpenPosition));
        StartCoroutine(MoveDoor(rightDoor, rightOpenPosition));
        isOpen = true;
    }

    private void CloseDoors()
    {
        StopAllCoroutines();
        StartCoroutine(MoveDoor(leftDoor, leftClosedPosition));
        StartCoroutine(MoveDoor(rightDoor, rightClosedPosition));
        isOpen = false;
    }

    private IEnumerator MoveDoor(Transform door, Vector3 targetPosition)
    {
        while (Vector3.Distance(door.localPosition, targetPosition) > 0.01f)
        {
            door.localPosition = Vector3.Lerp(door.localPosition, targetPosition, Time.deltaTime * doorSpeed);
            yield return null;
        }
        door.localPosition = targetPosition;
    }
}
