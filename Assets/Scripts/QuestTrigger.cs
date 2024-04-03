using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestionIdentifier
{
    Right,
    Wrong
}

public class QuestTrigger : MonoBehaviour
{
    public int questTrigger;
    public int score;
    public QuestionIdentifier identifier;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (QuestManager.Instance.currentQuest == questTrigger && identifier == QuestionIdentifier.Right)
            {
                QuestManager.Instance.ProceedQuest(questTrigger);
                QuestManager.Instance.UpdateScore(score);
                Destroy(this.gameObject);
            }
            else if(identifier == QuestionIdentifier.Wrong)
            {
                QuestManager.Instance.UpdateScore(-score);
                Destroy(this.gameObject);
            }
        }
    }
}