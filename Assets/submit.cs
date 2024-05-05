using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class submit : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text1,text2,text3;
    public VAFeedback va;
    public mendelManager men;
    public GameObject disable;


    public void checkAnswer()
    {
        Transform targetTransform = transform;
        if (text1.text == "Dominant" && text2.text == "Recessive" && text3.text == "Recessive")
        {
            va.RightAnswer(targetTransform);
            men.addSCore();
            disable.SetActive(false);
            men.currentQuestion++;
            men.displayQuestion();
        }
        else {va.WrongAnswer(targetTransform);
        men.minusScor();
        }


    }
}
