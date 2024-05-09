using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text keyText;


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
            coinText.SetText((score/10).ToString());
            headerText.text = "VICTORY!";
            winButton.SetActive(true);
        }
        else if (score <= 0)
        {
            headerText.text = "TRY AGAIN";
            coinText.SetText("0");
            keyText.SetText("0");
            badgeImage.sprite = badgeSprites[9];
            loseButton.SetActive(true);
        }
        else if (timeLeft <= 0)
        {
            headerText.text = "TIME'S UP!";
            coinText.SetText("0");
            keyText.SetText("0");
            badgeImage.sprite = badgeSprites[9];
            loseButton.SetActive(true);
        }

        scoreText.text = score.ToString("D4");
        Debug.Log("Score: " + score);

        Time.timeScale = 0.0f;
    }

    public void OnClick_TryAgainButton()
    {
        Time.timeScale = 1.0f;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void OnClick_NextSceneButton()
    {
        // GameManager.Instance.SaveData();

        Time.timeScale = 1.0f;

        SceneManager.LoadScene("laptop");
    }

    public void OnClick_MainMenuButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("laptop");
    }
}