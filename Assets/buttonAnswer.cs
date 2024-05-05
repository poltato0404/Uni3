using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class buttonAnswer : MonoBehaviour
{
    // Reference to the button's image component
    private Image buttonImage;
    [SerializeField]
    private TextMeshProUGUI text;

    // The toggle state to track the current color
    private bool isGreen = true;  // True if button is green, false if it's red

    // Colors for the button
    private Color greenColor = Color.green;  // Green color
    private Color redColor = Color.red;      // Red color

    void Start()
    {
        // Get the Image component from the button
        buttonImage = GetComponent<Image>();
        
        if (buttonImage != null)
        {
            // Set initial color to green
            buttonImage.color = greenColor;
        }
    }

    // Method to toggle the button's color
    public void ToggleColor()
    {
        if (buttonImage == null)
        {
            Debug.LogError("Button Image component is missing!");
            return;
        }

        // Toggle the boolean state
        isGreen = !isGreen;

        // Change the button's color based on the toggle state
        if (isGreen)
        {
            buttonImage.color = greenColor;
            text.text = "Dominant";
        }
        else
        {
            buttonImage.color = redColor;
            text.text = "Recessive";
            
        }
    }
}
