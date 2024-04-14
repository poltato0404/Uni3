using UnityEngine;

public class TPCameraControl : MonoBehaviour
{
    private const float yMin = -50f;
    private const float yMax = -4f;

    public Transform lookAt;

    public Vector3 offset;

    public Transform Player;

    public float distance = 10f;
    private float currentX = 0f;
    private float currentY = 0f;
    public float sensitivity = 4.0f;

    private void Start()
    {

    }

    private void LateUpdate()
    {
        currentX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        currentY += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        currentY = Mathf.Clamp(currentY, yMin, yMax);

        Vector3 Direction = new Vector3(offset.x, offset.y, -distance);
        Quaternion Rotation = Quaternion.Euler(-currentY, currentX, 0);

        transform.position = lookAt.position + Rotation * Direction;

        transform.LookAt(lookAt.position);
    }
}