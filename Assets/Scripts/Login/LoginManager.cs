using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;

public class LoginManager : MonoBehaviour, IDataPersistence
{
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    [SerializeField] private Image passwordVisibilityButtonImage;
    [SerializeField] private Sprite passwordVisibleSprite;
    [SerializeField] private Sprite passwordHiddenSprite;
    public TMP_Text errorMessageText;

    private GameData registerData;

    private bool isPasswordVisible = false;

    private void Start()
    {
        // Set the content type to Password initially
        SetContentType(TMP_InputField.ContentType.Password);
    }

    public void TogglePasswordFieldVisibility()
    {
        isPasswordVisible = !isPasswordVisible;

        if (isPasswordVisible)
        {
            SetContentType(TMP_InputField.ContentType.Standard);
            passwordVisibilityButtonImage.sprite = passwordVisibleSprite;
        }
        else
        {
            SetContentType(TMP_InputField.ContentType.Password);
            passwordVisibilityButtonImage.sprite = passwordHiddenSprite;
        }
    }

    private void SetContentType(TMP_InputField.ContentType contentType)
    {
        passwordField.contentType = contentType;

        if (contentType == TMP_InputField.ContentType.Password)
        {
            passwordField.inputType = TMP_InputField.InputType.Password;
        }
        else if (contentType == TMP_InputField.ContentType.Standard)
        {
            passwordField.inputType = TMP_InputField.InputType.Standard;
        }

        // Force update to reflect the changes in the input field
        passwordField.ForceLabelUpdate();
    }

    public void LoginUser()
    {
        string enteredUsername = usernameField.text;
        string enteredPassword = passwordField.text;

        // Check if the entered username exists in the registered data
        if (registerData != null && registerData.username == enteredUsername)
        {
            // Compare the entered password with the registered password
            string hashedEnteredPassword = HashPassword(enteredPassword);
            if (registerData.password == hashedEnteredPassword)
            {
                Debug.Log("Login successful!");

                // Load the main game scene
                SceneManager.LoadScene("main");
            }
            else
            {
                errorMessageText.text = "Incorrect password!"; // Display error message
            }
        }
        else
        {
            errorMessageText.text = "Username not found!"; // Display error message
        }
    }

    private string HashPassword(string password)
    {
        using (System.Security.Cryptography.SHA256 sha256Hash = System.Security.Cryptography.SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    // IDataPersistence interface methods
    public void LoadData(GameData data)
    {
        registerData = data;
    }

    public void SaveData(ref GameData data)
    {
        // You don't need to save data from the login manager
    }
}
