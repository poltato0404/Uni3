using UnityEngine;

public class DragPartsBehavior : MonoBehaviour
{
    public Vector3 offset;
    public string destinationTag = "DropZone";
    public CellParts parts;

    private Vector3 startPosition; // Initial position of the cell
    private Rotatable rotatableScript; // Reference to the Rotatable scriptG

    [SerializeField] GameObject drag;
    Isdragging dragStat;

    private void Start()
    {
        dragStat = drag.GetComponent<Isdragging>();
        startPosition = transform.position; // Assign the initial position
        rotatableScript = GetComponent<Rotatable>(); // Get reference to Rotatable script
    }

    public void OnMouseDown()
    {
        Debug.Log("Mouse Down");
        dragStat.isDragging = true;
        offset = transform.position - MouseWorldPosition();
        transform.GetComponent<Collider>().enabled = false;
        // Disable rotation when dragging starts
        if (rotatableScript != null)
        {
            rotatableScript.OnDragStart();

        }
    }

    public void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offset;
    }

    public void OnMouseUp()
    {
        dragStat.isDragging = false;
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            if (hitInfo.transform.tag == destinationTag)
            {
                // If parts match the correct one then positive feedback
                if (hitInfo.transform.gameObject.GetComponent<PartsMatch>().IsPartsMatch(parts))
                {
                    transform.position = hitInfo.transform.position;
                    transform.GetComponent<Collider>().enabled = false;
                    PartsObjectives.instance.correctMatches += 1;
                    PartsObjectives.instance.IncrementScore(20);
                }
                else
                {
                    // Incorrect match - reset to start position and decrement score
                    transform.position = startPosition;
                    transform.GetComponent<Collider>().enabled = true;
                    PartsObjectives.instance.DecrementScore(15);
                }
            }
            else
            {
                transform.GetComponent<Collider>().enabled = true;
            }
        }
        // Enable rotation when dragging ends
        if (rotatableScript != null)
        {
            rotatableScript.OnDragEnd();
        }
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}
