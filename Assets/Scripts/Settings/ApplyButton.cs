using UnityEngine;

public class ApplyButton : MonoBehaviour
{
    [SerializeField] private ResolutionControl resolutionControl;

    private void Start()
    {
        // Attach this script to the "Apply" button in the Unity editor
        // Link the ResolutionControl script to the "resolutionControl" field in the Unity editor
        if (resolutionControl == null)
        {
            Debug.LogError("ResolutionControl script not assigned to ApplyButton. Please assign it in the Unity editor.");
        }
    }

    public void OnApplyButtonClick()
    {
        // Call the ApplyResolution method from the linked ResolutionControl script
        if (resolutionControl != null)
        {
            resolutionControl.ApplyResolution();

            // Log a message indicating that the button is clicked
            Debug.Log("Apply button clicked!");
        }
    }
}