using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;

public class RegistrationManager : MonoBehaviour, IDataPersistence
{
    public TMP_InputField usernameField;
    public TMP_InputField nameField;
    public TMP_InputField passwordField;
    public TMP_InputField confirmPasswordField;
    public TMP_InputField securityQuesField;
    public TMP_InputField securityAnsField;
    [SerializeField] private Image passwordVisibilityButtonImage;
    [SerializeField] private Sprite passwordVisibleSprite;
    [SerializeField] private Sprite passwordHiddenSprite;

    private bool isPasswordVisible = false;
    private char hiddenCharacter = '●'; // Change this to '●' for circles or '*' for asterisks

    private GameData registerData;

    private void Start()
    {
        // Set the content type to Password initially
        SetContentType(TMP_InputField.ContentType.Password);
    }

    // Method to toggle the visibility of the password fields
    public void TogglePasswordVisibility()
    {
        isPasswordVisible = !isPasswordVisible;

        // Toggle the content type based on the visibility state
        if (isPasswordVisible)
        {
            SetContentType(TMP_InputField.ContentType.Standard);
        }
        else
        {
            SetContentType(TMP_InputField.ContentType.Password);
        }
    }

    // Method to set the content type and display character for the password fields
    private void SetContentType(TMP_InputField.ContentType contentType)
    {
        passwordField.contentType = contentType;
        confirmPasswordField.contentType = contentType;

        // Set the display character for password fields
        if (contentType == TMP_InputField.ContentType.Password)
        {
            passwordField.inputType = TMP_InputField.InputType.Password;
            confirmPasswordField.inputType = TMP_InputField.InputType.Password;
        }
        else if (contentType == TMP_InputField.ContentType.Standard)
        {
            passwordField.inputType = TMP_InputField.InputType.Standard;
            confirmPasswordField.inputType = TMP_InputField.InputType.Standard;
        }

        // Force update to reflect the changes in the input field
        passwordField.ForceLabelUpdate();
        confirmPasswordField.ForceLabelUpdate();
    }

    public void RegisterUser()
    {
        string username = usernameField.text;
        string name = nameField.text;
        string password = passwordField.text;
        string confirmPassword = confirmPasswordField.text;
        string securityQues = securityQuesField.text;
        string securityAns = securityAnsField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(securityQues) || string.IsNullOrEmpty(securityAns))
        {
            Debug.LogError("Please fill in all fields!");
            return;
        }

        if (password != confirmPassword)
        {
            Debug.LogError("Passwords do not match!");
            return;
        }

        // Here you would handle the registration logic using the registration data
        string hashedPassword = HashPassword(password);

        // Create a new GameData object to store registration data
        registerData = new GameData();
        registerData.username = username;
        registerData.name = name;
        registerData.password = hashedPassword;
        registerData.securityQuestion = securityQues;
        registerData.securityAnswer = securityAns;

        // Save the registration data
        SaveData(ref registerData);

        Debug.Log("User registered successfully!");

        SceneManager.LoadScene("Login");
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

    public void LoadData(GameData data)
    {
        // Implement loading data if needed
    }

    public void SaveData(ref GameData data)
    {
        data.username = usernameField.text;
        data.name = nameField.text;
        data.password = passwordField.text;
        data.securityQuestion = securityQuesField.text;
        data.securityAnswer = securityAnsField.text;

        registerData = data;
    }
}
