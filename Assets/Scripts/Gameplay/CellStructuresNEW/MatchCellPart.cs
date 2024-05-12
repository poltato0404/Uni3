using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchCellPart : MonoBehaviour
{
    public CellPart thisCellPart;
    public Image dropImage;

    // Adjusted scoring variables
    private int correctMatchBaseScore = 138; // Base score added for a correct match
    private int correctMatchExtraPoints = 172 / 6; // Extra points to distribute for each correct match
    private int incorrectMatchPenalty = 125; // Penalty deducted for an incorrect match

    public bool MatchCells(CellPart cellPart, Sprite sprite)
    {
        if (thisCellPart == cellPart)
        {
            dropImage.sprite = sprite;
            VAFeedback.Instance.RightAnswer(this.transform);

            int scoreToAdd = correctMatchBaseScore + correctMatchExtraPoints;
            CellObjectives.instance.AddMatches(1, scoreToAdd);

            this.enabled = false;
            return true;
        }
        else
        {
            VAFeedback.Instance.WrongAnswer(this.transform);
            CellObjectives.instance.AddMatches(0, -incorrectMatchPenalty);
            Debug.Log("Doesn't match");
            return false;
        }
    }
}
