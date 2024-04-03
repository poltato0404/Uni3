using UnityEngine;

public class DragCellBehavior : MonoBehaviour
{
    public Vector3 offset;
    public string destinationTag = "DropZone";
    public MitosisPhase phase;

    public Transform startPosition;


    public void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
        transform.GetComponent<Collider>().enabled = false;
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
                // If Phase matches the correct one then Positive Feedback
                // Else Negative feedback
                if (hitInfo.transform.gameObject.GetComponent<PhaseMatch>().IsPhaseMatch(phase))
                {
                    transform.position = hitInfo.transform.position;
                    transform.GetComponent<Collider>().enabled = false;
                    MitosisObjectives.instance.correctMatches += 1;
                    MitosisObjectives.instance.score += 20;
                }
                else
                {
                    if (MitosisObjectives.instance.score <= 0)
                        MitosisObjectives.instance.score = 0;
                    else
                        MitosisObjectives.instance.score -= 15;

                    transform.position = startPosition.position;
                    transform.GetComponent<Collider>().enabled = true;
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