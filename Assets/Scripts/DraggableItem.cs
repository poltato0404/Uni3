// DraggableItem.cs
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private DragAndDropGame game;

    // Additional properties specific to your draggable item
    public GameObject CorrectTarget { get; private set; }
    public bool IsMatched { get; private set; }

    private Vector2 originalPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void Initialize(DragAndDropGame gameReference, GameObject correctTarget)
    {
        game = gameReference;
        CorrectTarget = correctTarget;

        // Add any additional initialization logic here

        // For example, you might want to set initial positions, properties, etc.
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        SnapToTarget();
    }

    public void SnapToTarget()
    {
        UpdateMatchStatus(); // Update match status before snapping to target

        if (IsMatched)
        {
            // Handle correct match logic
            game.CorrectMatch(this);

            // Set the position of the draggable item to the target position
            rectTransform.anchoredPosition = CorrectTarget.GetComponent<RectTransform>().anchoredPosition;
        }
        else
        {
            // If not matched, return to the original position
            rectTransform.anchoredPosition = originalPosition;
        }
    }

    private void UpdateMatchStatus()
    {
        // Check if CorrectTarget is null before accessing its RectTransform
        if (CorrectTarget != null)
        {
            // Calculate if the distance between this item and the target is within a threshold
            IsMatched = (rectTransform.anchoredPosition - CorrectTarget.GetComponent<RectTransform>().anchoredPosition).magnitude < 50f;
        }
        else
        {
            // Handle the case where CorrectTarget is null (optional)
            IsMatched = false;
        }
    }
}
