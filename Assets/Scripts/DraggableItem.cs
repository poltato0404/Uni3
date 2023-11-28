using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private DragAndDropGame game;

    // Additional properties specific to your draggable item
    public bool IsOnCorrectTarget { get; private set; }
    public GameObject CorrectTarget { get; private set; }

    private Vector2 originalPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition;
        IsOnCorrectTarget = false;
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
        if (IsMatched())
        {
            // Handle correct match logic
            game.CorrectMatch(this);
        }
        else
        {
            // If not matched, return to the original position
            rectTransform.anchoredPosition = originalPosition;
        }
    }

    private bool IsMatched()
    {
        // Check if CorrectTarget is null before accessing its RectTransform
        if (CorrectTarget != null)
        {
            // Add logic to determine if this item is on the correct target
            IsOnCorrectTarget = (rectTransform.anchoredPosition - CorrectTarget.GetComponent<RectTransform>().anchoredPosition).magnitude < 50f;

            return IsOnCorrectTarget;
        }
        else
        {
            // Handle the case where CorrectTarget is null (optional)
            return false;
        }
    }
}
