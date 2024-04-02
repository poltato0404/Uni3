using GameEssentials.GameManager;
using System.Collections;
using TMPro;
using UnityEngine;

public class OrganMatchObjectives : MonoBehaviour
{
    // 5th minigame
    public int levelId = 4;

    public GameObject gameWinLose;

    public int matches = 0;
    public TextMeshProUGUI matchesText;

    public static OrganMatchObjectives instance;

    public int score;
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI questText;
    public float delayBetweenCharacters;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        UpdateMatches();
        UpdateScore();
    }

    void UpdateMatches()
    {
        matchesText.text = matches + "/7";
    }

    void UpdateScore()
    {
        scoreText.text = score.ToString("D4");
    }

    public void AddMatches(int _matches)
    {
        matches += _matches;
    }

    public void AddText(string text)
    {
        StartCoroutine(ShowText(text));
    }

    IEnumerator ShowText(string text)
    {
        questText.text = "";

        for (int i = 0; i < text.Length; i++)
        {
            questText.text += text[i];
            yield return new WaitForSeconds(delayBetweenCharacters);
        }

        if (matches >= 7)
        {
            if (score > 0)
            {
                GameManager.Instance.isLevelComplete[levelId] = true;
            }

            gameWinLose.SetActive(true);
        }
    }
}