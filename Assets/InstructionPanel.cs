using UnityEngine;
using TMPro;

public class TapToContinue : MonoBehaviour
{
    // Reference to the TextMeshPro object
    public TMP_Text tapToContinueText;
    // Reference to the panel
    public GameObject panel;

    // Update is called once per frame
    void Update()
    {
        // Check if the player taps the screen (for touch devices)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            // Hide the "Tap to continue" text and the panel
            tapToContinueText.gameObject.SetActive(false);
            panel.SetActive(false);
        }
        // Check if the player clicks the mouse button (for non-touch devices)
        else if (Input.GetMouseButtonDown(0))
        {
            // Hide the "Tap to continue" text and the panel
            tapToContinueText.gameObject.SetActive(false);
            panel.SetActive(false);
        }
    }
}
