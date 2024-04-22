using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;

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

    public GameObject popupUI;
    public TextMeshProUGUI popupText;

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
                                    VAFeedback.Instance.RightAnswer(hitInfo.transform);
                                    ShowPopup(0);
                                    //OrganMatchObjectives.instance.AddText(descriptionText[0]);
                                    OrganMatchObjectives.instance.ShowRiddle(3);
                                }
                                else
                                {
                                    if (OrganMatchObjectives.instance.score <= 0) OrganMatchObjectives.instance.score = 0;
                                    else OrganMatchObjectives.instance.score -= 5;
                                    VAFeedback.Instance.WrongAnswer(hitInfo.transform);
                                }
                                break;
                            case Organ.Kidney:
                                if (!matches[1])
                                {
                                    matches[1] = true;
                                    OrganMatchObjectives.instance.AddMatches(1);
                                    OrganMatchObjectives.instance.score += 10;
                                    VAFeedback.Instance.RightAnswer(hitInfo.transform);
                                    ShowPopup(2);
                                    OrganMatchObjectives.instance.ShowRiddle(6);
                                    //OrganMatchObjectives.instance.AddText(descriptionText[2]);
                                }
                                else
                                {
                                    if (OrganMatchObjectives.instance.score <= 0) OrganMatchObjectives.instance.score = 0;
                                    else OrganMatchObjectives.instance.score -= 5;
                                    VAFeedback.Instance.WrongAnswer(hitInfo.transform);
                                }
                                break;
                            case Organ.Liver:
                                if (!matches[2])
                                {
                                    matches[2] = true;
                                    OrganMatchObjectives.instance.AddMatches(1);
                                    OrganMatchObjectives.instance.score += 10;
                                    VAFeedback.Instance.RightAnswer(hitInfo.transform);
                                    ShowPopup(1);
                                    OrganMatchObjectives.instance.ShowRiddle(4);
                                    //OrganMatchObjectives.instance.AddText(descriptionText[1]);
                                }
                                else
                                {
                                    if (OrganMatchObjectives.instance.score <= 0) OrganMatchObjectives.instance.score = 0;
                                    else OrganMatchObjectives.instance.score -= 5;
                                    VAFeedback.Instance.WrongAnswer(hitInfo.transform);
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
                                VAFeedback.Instance.RightAnswer(hitInfo.transform);
                                ShowPopup(0);
                                OrganMatchObjectives.instance.ShowRiddle(0);
                                //OrganMatchObjectives.instance.AddText(descriptionText[0]);
                            }
                            else
                            {
                                if (OrganMatchObjectives.instance.score <= 0) OrganMatchObjectives.instance.score = 0;
                                else OrganMatchObjectives.instance.score -= 5;
                                VAFeedback.Instance.WrongAnswer(hitInfo.transform);
                            }
                        }
                        else
                        {
                            if (OrganMatchObjectives.instance.score <= 0) OrganMatchObjectives.instance.score = 0;
                            else OrganMatchObjectives.instance.score -= 5;
                            VAFeedback.Instance.WrongAnswer(hitInfo.transform);
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
                                VAFeedback.Instance.RightAnswer(hitInfo.transform);
                                ShowPopup(0);
                                OrganMatchObjectives.instance.ShowRiddle(1);
                                //OrganMatchObjectives.instance.AddText(descriptionText[0]);
                            }
                            else
                            {
                                if (OrganMatchObjectives.instance.score <= 0) OrganMatchObjectives.instance.score = 0;
                                else OrganMatchObjectives.instance.score -= 5;
                                VAFeedback.Instance.WrongAnswer(hitInfo.transform);
                            }
                        }
                        else
                        {
                            if (OrganMatchObjectives.instance.score <= 0) OrganMatchObjectives.instance.score = 0;
                            else OrganMatchObjectives.instance.score -= 5;
                            VAFeedback.Instance.WrongAnswer(hitInfo.transform);
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
                                    VAFeedback.Instance.RightAnswer(hitInfo.transform);
                                    ShowPopup(0);
                                    OrganMatchObjectives.instance.ShowRiddle(5);
                                    //OrganMatchObjectives.instance.AddText(descriptionText[0]);
                                }
                                else
                                {
                                    if (OrganMatchObjectives.instance.score <= 0) OrganMatchObjectives.instance.score = 0;
                                    else OrganMatchObjectives.instance.score -= 5;
                                    VAFeedback.Instance.WrongAnswer(hitInfo.transform);
                                }
                                break;
                            case Organ.Kidney:
                                if (!matches[1])
                                {
                                    matches[1] = true;
                                    OrganMatchObjectives.instance.AddMatches(1);
                                    OrganMatchObjectives.instance.score += 10;
                                    VAFeedback.Instance.RightAnswer(hitInfo.transform);
                                    ShowPopup(1);
                                    OrganMatchObjectives.instance.ShowRiddle(2);
                                    //OrganMatchObjectives.instance.AddText(descriptionText[1]);
                                }
                                else
                                {
                                    if (OrganMatchObjectives.instance.score <= 0) OrganMatchObjectives.instance.score = 0;
                                    else OrganMatchObjectives.instance.score -= 5;
                                    VAFeedback.Instance.WrongAnswer(hitInfo.transform);
                                }
                                break;
                            case Organ.Lung:
                                if (OrganMatchObjectives.instance.score <= 0) OrganMatchObjectives.instance.score = 0;
                                else OrganMatchObjectives.instance.score -= 5;
                                VAFeedback.Instance.WrongAnswer(hitInfo.transform);
                                break;
                        }
                        break;
                }
            }
            lineRenderer.enabled = false;
            isDrawing = false;
        }
    }


    void ShowPopup(int index)
    {
        OrganMatchObjectives.instance.ShowDescText(descriptionText[index]);
    }
}