using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Organ
{
    Heart,
    Lung,
    Kidney,
    Liver
}

public class DragMatchOrgans : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private bool isDrawing = false;
    public string destinationTag = "MatchPoint";

    public Organ organ;

    public bool[] matches;

    [TextArea(1,3)]
    public string[] descriptionText;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false; // Disable line renderer until mouse is pressed
    }

    void Update()
    {
        if (isDrawing)
        {
            UpdateLine();
        }
    }

    void UpdateLine()
    {
        // Update the line renderer positions
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, MouseWorldPosition());
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    void OnMouseDown()
    {
        // Enable line renderer and start drawing
        isDrawing = true;
        lineRenderer.enabled = true;
    }

    void OnMouseUp()
    {
        // Stop drawing
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            if (hitInfo.transform.tag == destinationTag)
            {
                switch (organ)
                {
                    case Organ.Heart:
                        switch (hitInfo.transform.gameObject.GetComponent<DragMatchOrgans>().organ)
                        {
                            case Organ.Lung:
                                if (!matches[0])
                                {
                                    matches[0] = true;
                                    OrganMatchObjectives.instance.AddMatches(1);
                                    OrganMatchObjectives.instance.score += 10;
                                    OrganMatchObjectives.instance.AddText(descriptionText[0]);
                                }
                                break;
                            case Organ.Kidney:
                                if (!matches[1])
                                {
                                    matches[1] = true;
                                    OrganMatchObjectives.instance.AddMatches(1);
                                    OrganMatchObjectives.instance.score += 10;
                                    OrganMatchObjectives.instance.AddText(descriptionText[2]);
                                }
                                break;
                            case Organ.Liver:
                                if (!matches[2])
                                {
                                    matches[2] = true;
                                    OrganMatchObjectives.instance.AddMatches(1);
                                    OrganMatchObjectives.instance.score += 10;
                                    OrganMatchObjectives.instance.AddText(descriptionText[1]);
                                }
                                break;
                        }
                        break;
                    case Organ.Lung:
                        if(hitInfo.transform.gameObject.GetComponent<DragMatchOrgans>().organ == Organ.Heart)
                        {
                            if (!matches[0])
                            {
                                matches[0] = true;
                                OrganMatchObjectives.instance.AddMatches(1);
                                OrganMatchObjectives.instance.score += 10;
                                OrganMatchObjectives.instance.AddText(descriptionText[0]);
                            }
                        }
                        else
                        {

                        }
                        break;
                    case Organ.Kidney:
                        if (hitInfo.transform.gameObject.GetComponent<DragMatchOrgans>().organ == Organ.Heart)
                        {
                            if (!matches[0])
                            {
                                matches[0] = true;
                                OrganMatchObjectives.instance.AddMatches(1);
                                OrganMatchObjectives.instance.score += 10;
                                OrganMatchObjectives.instance.AddText(descriptionText[0]);
                            }
                        }
                        else
                        {

                        }
                        break;
                    case Organ.Liver:
                        switch (hitInfo.transform.gameObject.GetComponent<DragMatchOrgans>().organ)
                        {
                            case Organ.Heart:
                                if (!matches[0])
                                {
                                    matches[0] = true;
                                    OrganMatchObjectives.instance.AddMatches(1);
                                    OrganMatchObjectives.instance.score += 10;
                                    OrganMatchObjectives.instance.AddText(descriptionText[0]);
                                }
                                break;
                            case Organ.Kidney:
                                if (!matches[1])
                                {
                                    matches[1] = true;
                                    OrganMatchObjectives.instance.AddMatches(1);
                                    OrganMatchObjectives.instance.score += 10;
                                    OrganMatchObjectives.instance.AddText(descriptionText[1]);
                                }
                                break;
                        }
                        break;
                }
            }
            lineRenderer.enabled = false;
            isDrawing = false;
        }
        
    }
}