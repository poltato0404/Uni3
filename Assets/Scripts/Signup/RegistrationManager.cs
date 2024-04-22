using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;

public class RegistrationManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] private TMP_InputField confirmPasswordField;
    [SerializeField] private TMP_InputField securityQuesField;
    [SerializeField] private TMP_InputField securityAnsField;

    private List<GameData> registerData = new List<GameData>();

    public void RegisterUser()
    {
        string username = usernameField.text;
        string name = nameField.text;
        string password = passwordField.text;
        string confirmPassword = confirmPasswordField.text;
        string securityQues = securityQuesField.text;
        string securityAns = securityAnsField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(securityQues) || string.IsNullOrEmpty(securityAns))
        {
            Debug.LogError("Please fill in all fields!");
            return;
        }

        if (password != confirmPassword)
        {
            Debug.LogError("Passwords do not match!");
            return;
        }

        // Check if the username already exists
        if (UserExists(username))
        {
            Debug.LogError("Username already exists!");
            return;
        }

        // Here you would handle the registration logic using the registration data
        string hashedPassword = HashPassword(password);

        // Create a new GameData object to store registration data
        GameData newUser = new GameData();
        newUser.username = username;
        newUser.name = name;
        newUser.password = password;
        newUser.securityQuestion = securityQues;
        newUser.securityAnswer = securityAns;

        // Pass the new user data to the SaveData method
        SaveData(ref newUser);

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

    private bool UserExists(string username)
    {
        foreach (GameData user in registerData)
        {
            if (user.username == username)
            {
                return true;
            }
        }
        return false;
    }

    public void LoadData(GameData data)
    {
        // Implement loading data if needed
    }

    public void SaveData(ref GameData data)
    {
        registerData.Add(data);
    }
}