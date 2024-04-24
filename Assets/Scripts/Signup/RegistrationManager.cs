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
    [SerializeField] private Image confirmPasswordVisibilityButtonImage;
    [SerializeField] private Sprite passwordVisibleSprite;
    [SerializeField] private Sprite passwordHiddenSprite;

    private bool isPasswordVisible = false;
    private bool isConfirmPasswordVisible = false;
    private char hiddenCharacter = '●';
    public TMP_Text errorMessageText;

    private GameData registerData;

    private void Start()
    {
        // Set the content type to Password initially
        SetContentType(TMP_InputField.ContentType.Password, passwordField);
        SetContentType(TMP_InputField.ContentType.Password, confirmPasswordField);
    }

    public void TogglePasswordFieldVisibility()
    {
        isPasswordVisible = !isPasswordVisible;

        if (isPasswordVisible)
        {
            SetContentType(TMP_InputField.ContentType.Standard, passwordField);
            passwordVisibilityButtonImage.sprite = passwordVisibleSprite;
        }
        else
        {
            SetContentType(TMP_InputField.ContentType.Password, passwordField);
            passwordVisibilityButtonImage.sprite = passwordHiddenSprite;
        }
    }

    public void ToggleConfirmPasswordFieldVisibility()
    {
        isConfirmPasswordVisible = !isConfirmPasswordVisible;

        if (isConfirmPasswordVisible)
        {
            SetContentType(TMP_InputField.ContentType.Standard, confirmPasswordField);
            confirmPasswordVisibilityButtonImage.sprite = passwordVisibleSprite;
        }
        else
        {
            SetContentType(TMP_InputField.ContentType.Password, confirmPasswordField);
            confirmPasswordVisibilityButtonImage.sprite = passwordHiddenSprite;
        }
    }

    private void SetContentType(TMP_InputField.ContentType contentType, TMP_InputField inputField)
    {
        inputField.contentType = contentType;

        if (contentType == TMP_InputField.ContentType.Password)
        {
            inputField.inputType = TMP_InputField.InputType.Password;
        }
        else if (contentType == TMP_InputField.ContentType.Standard)
        {
            inputField.inputType = TMP_InputField.InputType.Standard;
        }

        // Force update to reflect the changes in the input field
        inputField.ForceLabelUpdate();
    }

    public void RegisterUser()
    {
        // Clear previous error messages
        errorMessageText.text = "";

        string username = usernameField.text;
        string name = nameField.text;
        string password = passwordField.text;
        string confirmPassword = confirmPasswordField.text;
        string securityQues = securityQuesField.text;
        string securityAns = securityAnsField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(securityQues) || string.IsNullOrEmpty(securityAns))
        {
            errorMessageText.text = "Please fill in all fields!";
            return;
        }

        if (password != confirmPassword)
        {
            errorMessageText.text = "Passwords do not match!";
            return;
        }

        // Handle the registration logic using the registration data
        string hashedPassword = HashPassword(password);

        // Create a new GameData object to store registration data
        registerData = new GameData();
        registerData.username = username;
        registerData.name = name;
        registerData.password = hashedPassword;
        registerData.securityQuestion = securityQues;
        registerData.securityAnswer = securityAns;

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
        data.password = HashPassword(passwordField.text); // Store hashed password
        data.securityQuestion = securityQuesField.text;
        data.securityAnswer = securityAnsField.text;

        registerData = data;
    }
}