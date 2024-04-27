using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAroundPivot : MonoBehaviour
{
    public Transform pivotPoint;
    public float rotationSpeed = 1f;
    public float minYAngle = -90f;
    public float maxYAngle = 90f;

    private Vector3 lastMousePosition;
    public bool isUIBeingDragged = false;

    public static DragAroundPivot instance;

    public CellObjectives cellObjectives;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!isUIBeingDragged && !cellObjectives.isWin)
        {
            if (Input.touchCount > 0)
            {
                // Get the first touch
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        lastMousePosition = touch.position;
                        break;

                    case TouchPhase.Moved:
                        // Introduce a delay before rotation starts
                        if (Time.timeSinceLevelLoad > 0.1f) // Adjust the delay as needed
                        {
                            Vector2 delta = touch.position - (Vector2)lastMousePosition;
                            float rotationX = delta.y * rotationSpeed;
                            float rotationY = -delta.x * rotationSpeed;

                            // Rotate the camera around the pivot point
                            transform.RotateAround(pivotPoint.position, Vector3.up, -rotationY);
                            transform.RotateAround(pivotPoint.position, transform.right, -rotationX);

                            lastMousePosition = touch.position;

                            // Get the current rotation
                            Vector3 currentRotation = transform.localEulerAngles;

                            // Calculate the clamped rotation around the X-axis
                            float clampedXAngle = currentRotation.x - rotationX;
                            clampedXAngle = Mathf.Clamp(clampedXAngle, minYAngle, maxYAngle);

                            // If the clamped angle is at the limit, stop further rotation
                            if (clampedXAngle == minYAngle || clampedXAngle == maxYAngle)
                            {
                                transform.RotateAround(pivotPoint.position, Vector3.up, -rotationY);
                                transform.RotateAround(pivotPoint.position, transform.right, rotationX);
                            }
                        }
                        break;

                    case TouchPhase.Ended:
                        lastMousePosition = Vector3.zero;
                        break;
                }
            }
        }
    }
}