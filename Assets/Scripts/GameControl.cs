using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    GameObject token;
    List<int> faceIndexes = new List<int> { 0, 1, 2, 3, 0, 1, 2, 3 };
    public static System.Random rnd = new System.Random();
    public int shuffleNum = 0;
    int[] visibleFaces = { -1, -2 };
    public float timer = 60.0f;
    public int score = 0;

    void Start()
    {
        int originalLength = faceIndexes.Count;
        float yPosition = 2.3f;
        float xPosition = -2.2f;
        StartCoroutine(Countdown());
        for (int i = 0; i < 7; i++)
        {
            shuffleNum = rnd.Next(0, (faceIndexes.Count));
            var temp = Instantiate(token, new Vector3(
                xPosition, yPosition, 0),
                Quaternion.identity);
            temp.GetComponent<MainToken>().faceIndex = faceIndexes[shuffleNum];
            faceIndexes.Remove(faceIndexes[shuffleNum]);
            xPosition = xPosition + 4;
            if (i == (originalLength / 2 - 2))
            {
                yPosition = -2.3f;
                xPosition = -6.2f;
            }
        }
        token.GetComponent<MainToken>().faceIndex = faceIndexes[0];
    }

    IEnumerator Countdown()
    {
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        // Handle time's up scenario
        Debug.Log("Time's up!");
    }

    public bool TwoCardsUp()
    {
        bool cardsUp = false;
        if (visibleFaces[0] >= 0 && visibleFaces[1] >= 0)
        {
            cardsUp = true;
            score++;
            Debug.Log("Score: " + score);
        }
        return cardsUp;
    }

    public void AddVisibleFace(int index)
    {
        if (visibleFaces[0] == -1)
        {
            visibleFaces[0] = index;
        }
        else if (visibleFaces[1] == -2)
        {
            visibleFaces[1] = index;
        }
    }

    public void RemoveVisibleFace(int index)
    {
        // Implement removal logic here
    }

    public bool CheckMatch()
    {
        bool success = false;
        if (visibleFaces[0] == visibleFaces[1])
        {
            visibleFaces[0] = -1;
            visibleFaces[1] = -2;
            success = true;
        }
        return success;
    }
}
