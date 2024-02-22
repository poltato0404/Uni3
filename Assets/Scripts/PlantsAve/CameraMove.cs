using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 5f, -10f);
    public float smoothSpeed = 0.5f;

    void Update()
    {
        if (player == null)
        {
            Debug.LogError("Player reference not set in FollowPlayer script!");
            return;
        }

        // Calculate the desired position
        Vector3 desiredPosition = player.position + offset;

        // Smoothly interpolate between the current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Set the camera's position to the smoothed position
        transform.position = smoothedPosition;
    }
}
