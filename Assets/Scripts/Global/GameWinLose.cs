using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using GameEssentials.GameManager;

public class GameWinLose : MonoBehaviour
{
    public int minigameID;

    public static GameWinLose Instance;

    public GameObject winButton, loseButton;

    public TextMeshProUGUI headerText;

    public int score;
    public float timeLeft;

    public GameObject[] gameObjectsToDisable;

    public Image badgeImage;
    public Sprite[] badgeSprites;


    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void OnEnable()
    {
        OnGamePreExit();
    }

    public void OnGamePreExit()
    {
        foreach (GameObject go in gameObjectsToDisable)
        {
            go.SetActive(false);
        }

        if (score > 0 && timeLeft > 0)
        {
            badgeImage.gameObject.SetActive(true);
            badgeImage.sprite = badgeSprites[minigameID];
            headerText.text = "VICTORY!";
            winButton.SetActive(true);
        }
        else if (score <= 0)
        {
            headerText.text = "TRY AGAIN";
            badgeImage.sprite = badgeSprites[9];
            loseButton.SetActive(true);
        }
        else if (timeLeft <= 0)
        {
            headerText.text = "TIME'S UP!";
            badgeImage.sprite = badgeSprites[9];
            loseButton.SetActive(true);
        }

        scoreText.text = score.ToString("D4");

        Time.timeScale = 0.0f;
    }


    public void OnClick_TryAgainButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClick_NextSceneButton()
    {
        // GameManager.Instance.SaveData();

        Time.timeScale = 1.0f;

        Debug.Log(SceneManager.GetActiveScene().buildIndex);

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 3:
            case 5:
            case 7:
                SceneManager.LoadScene(0);
                break;
            default:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
        }
    }

    public void OnClick_MainMenuButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("laptop");
    }
}