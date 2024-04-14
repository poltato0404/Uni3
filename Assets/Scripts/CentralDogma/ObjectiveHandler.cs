using GameEssentials.GameManager;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveHandler : MonoBehaviour
{
    // 3rd minigame
    public int levelId = 2;

    public GameObject gameWinLose;

    [Header("Gameplay Objectives")]
    public GameObject polypeptideCubes;
    public GameObject[] polypetideStrands;
    public GameObject mRNAStrand;
    public GameObject RNAStrand;
    public GameObject[] nucleotideSpawner;

    public static ObjectiveHandler instance;

    public int score;
    public TextMeshProUGUI scoreText;

    [Header("Timer Properties")]
    public float totalTime;
    private float currentTime;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private int matchCount;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        currentTime = totalTime;
    }

    private void OnEnable()
    {
        CodonMatch.OnRightMatch += OnMatchAddCount;
    }

    private void OnDisable()
    {
        CodonMatch.OnRightMatch -= OnMatchAddCount;
    }

    private void OnMatchAddCount(int count)
    {
        matchCount += count;

        if(matchCount == 12 )
        {
            PrepareNextObjective();
        }
        
        if(matchCount == 16)
        {
            if (score > 0)
            {
                GameManager.Instance.isLevelComplete[levelId] = true;
            }

            gameWinLose.gameObject.GetComponent<GameWinLose>().score = score;

            gameWinLose.SetActive(true);
        }
    }

    void PrepareNextObjective()
    {
        mRNAStrand.transform.position = new Vector3(mRNAStrand.transform.position.x, -3, mRNAStrand.transform.position.z);
        RNAStrand.SetActive(false);

        polypeptideCubes.SetActive(true);

        foreach (GameObject obj in polypetideStrands)
        {
            obj.SetActive(true);
        }

        foreach(GameObject obj in nucleotideSpawner)
        {
            obj.SetActive(false);
        }
    }


    private void Update()
    {
        CountdownTimer();
        ScoreCounter();
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
            //Lose
            if (score > 0)
            {
                gameWinLose.gameObject.GetComponent<GameWinLose>().score = score;
            }

            gameWinLose.SetActive(true);
            //Debug.Log("Timer Ran out!");
        }
    }
}