using UnityEngine;

public class rotate : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public GameObject item;

    void Update()
    {
        // Rotate the object around its Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
