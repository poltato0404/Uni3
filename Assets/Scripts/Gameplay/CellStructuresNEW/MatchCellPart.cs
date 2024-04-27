using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchCellPart : MonoBehaviour
{
    public CellPart thisCellPart;
    public Image dropImage;
    public CellObjectives cell;

    public bool MatchCells(CellPart cellPart, Sprite sprite)
    {
        if (thisCellPart == cellPart)
        {
            dropImage.sprite = sprite;
            VAFeedback.Instance.RightAnswer(this.transform);

            cell.AddMatches(1, 50);

            this.enabled = false;
            return true;
        }
        else
        {
            VAFeedback.Instance.WrongAnswer(this.transform);
            cell.AddMatches(0, -25);
            Debug.Log("Doesn't match");
            return false;
        }
    }
}