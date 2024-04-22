using UnityEngine;
using UnityEngine.UI;

public class RotateAndScaleSlider : MonoBehaviour
{
    public Slider rotationXSlider;
    public Slider rotationYSlider;
    public Slider scaleSlider;

    void Start()
    {
        UpdateTransform();
    }

    void Update()
    {
        UpdateTransform();
    }

    private void UpdateTransform()
    {
        // Calculate rotation based on both X and Y sliders
        float angleX = rotationXSlider.value * 360f;
        float angleY = rotationYSlider.value * 360f;

        // Apply the calculated rotation
        transform.rotation = Quaternion.Euler(angleX, angleY, 0);

        // Apply the scale uniformly across all dimensions
        float scaleValue = scaleSlider.value;
        transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
    }
}
