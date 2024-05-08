using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;

public class LoginManager : MonoBehaviour, IDataPersistence
{
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public TMP_Text errorMessageText;
    public Button loginButton; // Reference to the login button

    private GameData registerData;

    private void Start()
    {
        // Disable the login button initially
        loginButton.interactable = false;
    }

    public void LoginUser()
    {
        // Check if both fields are filled
        if (!string.IsNullOrEmpty(usernameField.text) && !string.IsNullOrEmpty(passwordField.text))
        {
            string enteredUsername = usernameField.text;
            string enteredPassword = passwordField.text;

            // Your existing login logic goes here...
        }
        else
        {
            errorMessageText.text = "Please enter both username and password!";
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
