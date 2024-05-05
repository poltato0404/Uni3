using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset; // Offset between the object and the drag point
    private Vector3 startPosition; // The starting position of the object
    public Color rayColor = Color.red; // Color of the debug ray
    public float rayLength = 100f;
    public float dragRange = 1f; // Range within which the object snaps to the holder
    public GameObject holder;
    public float returnSpeed = 5f; // Speed at which the object returns to the start position
    public int chickenNumber;
    ChickenTraits chick;
    plateScript ps;
    [SerializeField] bool plateSafe;

    void Start()
    {
        // Store the initial position when the object is created
        startPosition = transform.position;
        holder = GameObject.FindWithTag("plate");
        ps = holder.GetComponent<plateScript>();
        dragRange = 1f; 
        plateSafe = true;
        chick = GetComponent<ChickenTraits>();
    }

    void Update()
    {
        
        // Handle touch and mouse input
        if (Input.touchCount > 0)
        {
            HandleTouchInput();
        }
        else if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
        {
            HandleMouseInput();
        }

        // If not dragging, move back to the start position
        if (!isDragging && !IsNearHolder())
        {
            ReturnToStartPosition();
        }
        if (!isDragging && IsNearHolder() && plateSafe)
        {
            
            goToPlate();   
            plateSafe = false;
        }
        

    }
    private void goToPlate()
    {
        Vector3 plateTop = new Vector3 (0, 1f, 7f);
        transform.position = plateTop;
        ps.addChicken(chick);
        
    }
    private bool IsNearHolder()
    {
        if (holder == null) return false; // Safety check
        return Vector3.Distance(transform.position, holder.transform.position) < dragRange; // Check if within range
    }

    private void HandleTouchInput()
    {
        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                HandleBegin(GetWorldPoint(touch.position));
                break;

            case TouchPhase.Moved:
                if (isDragging)
                {
                    HandleMove(GetWorldPoint(touch.position));
                }
                break;

            case TouchPhase.Ended:
                isDragging = false;
                break;
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleBegin(MouseWorldPosition());
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            HandleMove(MouseWorldPosition());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    private void HandleBegin(Vector3 worldPoint)
    {
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit hit;

        Debug.DrawRay(rayOrigin, rayDirection * rayLength, rayColor, 1f);

        if (Physics.Raycast(rayOrigin, rayDirection, out hit))
        {
            if (hit.transform == this.transform)
            {
                isDragging = true;

                
            }
        }
    }

    private void HandleMove(Vector3 worldPoint)
    {
        transform.position = worldPoint + offset; // Update position with offset to maintain correct drag
    }

    private Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z; // Get z-distance
        return Camera.main.ScreenToWorldPoint(mouseScreenPos); // Convert to world position
    }

    private Vector3 GetWorldPoint(Vector2 screenPoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        float enter;
        Plane plane = new Plane(Vector3.forward, 0);

        if (plane.Raycast(ray, out enter))
        {
            return ray.GetPoint(enter);
        }

        return Vector3.zero; // Fallback
    }

    private void ReturnToStartPosition()
    {
        // Smoothly move the object back to the starting position
        transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime * returnSpeed);
    }
}
