using UnityEngine;

public class PauseTimeOnPanel : MonoBehaviour
{
    public GameObject panelToShow;

    void Update()
    {
        // Check if the panel is active
        if (panelToShow.activeSelf)
        {
            // Pause the time
            Time.timeScale = 0f;
        }
        else
        {
            // Unpause the time
            Time.timeScale = 1f;
        }
    }

    // This method is called when the "okay" button is pressed
    public void OnOkayButtonPressed()
    {
        // Hide the panel
        panelToShow.SetActive(false);

        // Resume the time
        Time.timeScale = 1f;
    }
}
