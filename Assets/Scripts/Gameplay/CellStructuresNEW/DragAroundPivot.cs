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

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!isUIBeingDragged && !CellObjectives.instance.isWin)
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                // Introduce a delay before rotation starts
                if (Time.timeSinceLevelLoad > 0.1f) // Adjust the delay as needed
                {
                    Vector3 delta = Input.mousePosition - lastMousePosition;
                    float rotationX = delta.y * rotationSpeed;
                    float rotationY = -delta.x * rotationSpeed;

                    // Rotate the camera around the pivot point
                    transform.RotateAround(pivotPoint.position, Vector3.up, -rotationY);
                    transform.RotateAround(pivotPoint.position, transform.right, -rotationX);

                    lastMousePosition = Input.mousePosition;

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
            }

            if (Input.GetMouseButtonUp(0))
            {
                lastMousePosition = Vector3.zero;
            }
        }
    }
}