using GameEssentials.GameManager;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class CardHolder : MonoBehaviour
{
    // 6th minigame
    public int levelId = 5;

    public GameObject gameWinLose;

    public TextMeshProUGUI[] cardText;

    public string[] cardAnswer;
    public string[] cardDesc;

    public Sprite[] answerImages;
    public Image[] answerImage;

    public CardBehaviour firstCard;
    public CardBehaviour secondCard;

    public static CardHolder instance;

    public GameObject[] cards;

    [SerializeField] private int matchesMade = 0;

    [HideInInspector] public bool canFlipCard = true;

    public int score;
    public TextMeshProUGUI scoreText;

    [Header("Timer Properties")]
    public float totalTime;
    private float currentTime;
    [SerializeField] private TextMeshProUGUI timerText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentTime = totalTime;
        CardSetup();
    }

    void CardSetup()
    {
        bool textSet = false;

        for (int i = 0; i < cardAnswer.Length; i++)
        {
            for (int j = 0; j < cardText.Length; j++)
            {
                if (string.IsNullOrEmpty(cardText[j].text)) // Check if text is empty or null
                {
                    cardText[j].text = cardAnswer[i];
                    answerImage[j].sprite = answerImages[i];
                    cardText[j].gameObject.GetComponentInParent<CardBehaviour>().matchID = i;
                    textSet = true;
                    break; // Break out of the inner loop once text is set
                }
            }
            if (!textSet)
            {
                // All text fields are already set, handle this case if necessary
                Debug.LogWarning("All text fields are already set.");
                break; // Break out of the outer loop if all text fields are set
            }
            textSet = false; // Reset the flag for the next iteration
        }

        for (int i = 0; i < cardDesc.Length; i++)
        {
            for (int j = 0; j < cardText.Length; j++)
            {
                if (string.IsNullOrEmpty(cardText[j].text)) // Check if text is empty or null
                {
                    cardText[j].text = cardDesc[i];
                    cardText[j].gameObject.GetComponentInParent<CardBehaviour>().matchID = i;
                    textSet = true;
                    break; // Break out of the inner loop once text is set
                }
            }
            if (!textSet)
            {
                // All text fields are already set, handle this case if necessary
                Debug.LogWarning("All text fields are already set.");
                break; // Break out of the outer loop if all text fields are set
            }
            textSet = false; // Reset the flag for the next iteration
        }

        ShuffleCards();
    }

    void ShuffleCards()
    {
        System.Random rng = new System.Random();
        int n = cards.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);

            // Swap positions of cards[k] and cards[n]
            Vector3 tempPosition = cards[k].transform.position;
            cards[k].transform.position = cards[n].transform.position;
            cards[n].transform.position = tempPosition;
        }
    }

    public void SetCard(CardBehaviour card)
    {
        if (canFlipCard)
        {
            if (firstCard == null)
            {
                firstCard = card;
                firstCard.canFlipThisCard = false;
            }
            else if (secondCard == null)
            {
                secondCard = card;
                secondCard.canFlipThisCard = false;
                StartCoroutine(CheckMatch(firstCard, secondCard));
            }
        }
    }

    public bool CheckMatching()
    {
        if (firstCard == null || secondCard == null) return true;
        else return false;
    }

    IEnumerator CheckMatch(CardBehaviour card1, CardBehaviour card2)
    {
        yield return new WaitForSeconds(0.5f);

        if (card1 != null && card2 != null)
        {
            if (card1.matchID == card2.matchID && card1.cardNumber != card2.cardNumber)
            {
                firstCard = null;
                secondCard = null;

                yield return new WaitForSeconds(0.3f);

                VAFeedback.Instance.RightAnswer(card1.transform);

                Destroy(card1.gameObject);
                Destroy(card2.gameObject);

                score += 250;
                

                matchesMade += 1;

                if (matchesMade >= 4)
                {
                    GameManager.Instance.isLevelComplete[levelId] = true;

                    gameWinLose.GetComponent<GameWinLose>().timeLeft = currentTime;
                    gameWinLose.gameObject.GetComponent<GameWinLose>().score = score;

                    gameWinLose.SetActive(true);
                }
            }
            else if (card1.matchID != card2.matchID)
            {
                if (score <= 0) score = 0;
                else score -= 50;

                VAFeedback.Instance.WrongAnswer(card2.transform);

                card1.FlipBack();
                card2.FlipBack();
                firstCard = null;
                secondCard = null;
            }
        }
        else
        {
            // Handle the case when one or both cards are already destroyed
            Debug.LogWarning("One or both cards have been destroyed.");
        }
    }

    private void Update()
    {
        ScoreCounter();
        CountdownTimer();
    }

    private void ScoreCounter()
    {
        scoreText.text = score.ToString("D4");
    }

    private void CountdownTimer()
    {
        currentTime -= Time.deltaTime;

        currentTime = Mathf.Clamp(currentTime, 0, totalTime);

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);


        if (currentTime <= 0f)
        {
            //Lose regardless of score
            gameWinLose.GetComponent<GameWinLose>().timeLeft = currentTime;
            gameWinLose.GetComponent<GameWinLose>().score = score;
            gameWinLose.SetActive(true);
            //Debug.Log("Timer Ran out!");
        }
    }
}