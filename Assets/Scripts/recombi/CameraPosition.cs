using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    void Start()
    {
        // Set the camera to orthographic projection
        Camera.main.orthographic = true;

        // Set the camera's position
        transform.position = new Vector3(0f, 10f, 0f);

        // Set the orthographic size for zoom level
        Camera.main.orthographicSize = 5f;
    }
}
