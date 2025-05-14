using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Referencias")]
    public Animator doorAnimator;
    public string openTrigger = "Open";
    public string closeTrigger = "Close";
    public float openDistance = 3f;

    [Header("Estado")]
    public bool isOpen = false;
    public bool puzzleSolved = false;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (puzzleSolved && !isOpen && IsPlayerNear())
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        if (!isOpen && doorAnimator != null)
        {
            doorAnimator.SetTrigger(openTrigger);
            isOpen = true;
        }
    }

    public void CloseDoor()
    {
        if (isOpen && doorAnimator != null)
        {
            doorAnimator.SetTrigger(closeTrigger);
            isOpen = false;
        }
    }

    public void PuzzleSolved()
    {
        puzzleSolved = true;
    }

    private bool IsPlayerNear()
    {
        return Vector3.Distance(transform.position, player.position) <= openDistance;
    }
}