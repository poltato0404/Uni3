using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    // Would implement a proper pause function but since we don't have
    // much animation to keep track or pause, this will suffice for now

    public void OnEnable()
    {
        Time.timeScale = 0.0f;
    }

    public void OnDisable()
    {
        Time.timeScale = 1.0f;
    }


    public void OnClick_MainMenuButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void OnClick_RestartButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}