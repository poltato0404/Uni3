using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour, IDataPersistence
{
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;

    private GameData registerData;
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
                Debug.LogError("Incorrect password!");
            }
        }
        else
        {
            Debug.LogError("Username not found!");
        }
    }

    private string HashPassword(string password)
    {
        // Your password hashing logic here
        return password; // For demonstration, simply return the password as is
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