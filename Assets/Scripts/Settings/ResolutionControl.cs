using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionControl : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private int selectedResolutionIndex = 0;

    void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();

        // Print available resolutions to the console for debugging
        foreach (Resolution resolution in resolutions)
        {
            Debug.Log($"Available Resolution: {resolution.width}x{resolution.height} @{resolution.refreshRate}Hz");
            filteredResolutions.Add(resolution);
        }

        // Debug statement to log the number of filtered resolutions
        Debug.Log($"Number of filtered resolutions: {filteredResolutions.Count}");

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = $"{filteredResolutions[i].width}x{filteredResolutions[i].height} {filteredResolutions[i].refreshRate}Hz";
            options.Add(resolutionOption);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                selectedResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = selectedResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        // Store the selected resolution index
        selectedResolutionIndex = resolutionIndex;
    }

    public void ApplyResolution()
    {
        // Apply the selected resolution
        Resolution resolution = filteredResolutions[selectedResolutionIndex];
        Screen.SetResolution(resolution.height, resolution.width, true);

        // Log a message indicating that the resolution has been changed
        Debug.Log($"Resolution changed to: {resolution.height}x{resolution.width} @{resolution.refreshRate}Hz");
    }

}
