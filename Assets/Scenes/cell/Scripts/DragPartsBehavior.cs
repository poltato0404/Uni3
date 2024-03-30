using UnityEngine;

public class DragCellBehavior : MonoBehaviour
{
    public Vector3 offset;
    public string destinationTag = "DropZone";
    public CellParts parts;

    private Transform startPosition; // Initial position of the cell

    private void Start()
    {
        startPosition = transform; // Assign the initial position
    }

    public void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
        transform.GetComponent<Collider>().enabled = false;
        startPosition = transform; // Reassign the start position
    }

    public void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offset;
    }

    public void OnMouseUp()
    {
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
                    transform.position = startPosition.position;
                    transform.GetComponent<Collider>().enabled = true;
                    PartsObjectives.instance.DecrementScore(15);
                }
            }
            else
            {
                transform.GetComponent<Collider>().enabled = true;
            }
        }
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}
