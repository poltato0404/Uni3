using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragUIImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool dragOnSurfaces = true;

    public CellPart thisCellPart;

    private GameObject m_DraggingIcon;
    private RectTransform m_DraggingPlane;

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragAroundPivot.instance.isUIBeingDragged = true;
        var canvas = FindInParents<Canvas>(gameObject);
        if (canvas == null)
            return;

        // We have clicked something that can be dragged.
        // What we want to do is create an icon for this.
        m_DraggingIcon = new GameObject("icon");

        m_DraggingIcon.transform.SetParent(canvas.transform, false);
        m_DraggingIcon.transform.SetAsLastSibling();

        var image = m_DraggingIcon.AddComponent<Image>();

        image.sprite = GetComponent<Image>().sprite;
        image.SetNativeSize();

        if (dragOnSurfaces)
            m_DraggingPlane = transform as RectTransform;
        else
            m_DraggingPlane = canvas.transform as RectTransform;

        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData data)
    {
        if (m_DraggingIcon != null)
            SetDraggedPosition(data);
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
            m_DraggingPlane = data.pointerEnter.transform as RectTransform;

        var rt = m_DraggingIcon.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
            rt.rotation = m_DraggingPlane.rotation;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (m_DraggingIcon != null)
            Destroy(m_DraggingIcon);

        DragAroundPivot.instance.isUIBeingDragged = false;
    }

    static public T FindInParents<T>(GameObject go) where T : Component
    {
        if (go == null) return null;
        var comp = go.GetComponent<T>();

        if (comp != null)
            return comp;

        Transform t = go.transform.parent;
        while (t != null && comp == null)
        {
            comp = t.gameObject.GetComponent<T>();
            t = t.parent;
        }
        return comp;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DragAroundPivot.instance.isUIBeingDragged = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        DragAroundPivot.instance.isUIBeingDragged = false;
        
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Raycast from the pointer position
            Ray ray = Camera.main.ScreenPointToRay(eventData.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object has the MatchCellPart component
                MatchCellPart hitMatchCellPart = hit.collider.GetComponent<MatchCellPart>();
                if (hitMatchCellPart != null)
                {
                    // Do something with the hitMatchCellPart (e.g., call a method)
                    if(hitMatchCellPart.MatchCells(thisCellPart, GetComponent<Image>().sprite))
                    {
                        this.enabled = false;
                        var image = GetComponent<Image>();
                        var tempColor = image.color;
                        tempColor.r = 0;
                        tempColor.g = 255;
                        tempColor.b = 0;
                        tempColor.a = 0.5f;
                        image.color = tempColor;
                        if (m_DraggingIcon != null)
                            Destroy(m_DraggingIcon);
                    }
                }
            }
        }
        
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}