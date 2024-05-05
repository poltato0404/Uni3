using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class secondQuestion : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text1,text2,text3,text4,text5,text6;
    public VAFeedback va;
    public mendelManager men;
    public GameObject disable;
    


    public void checkAnswer()
    {
        Transform targetTransform = transform;
        if (text1.text == "F" && text2.text == "f" && text3.text == "B" && text4.text == "b" && text5.text == "c" && text6.text == "c")
        {
            va.RightAnswer(targetTransform);
            Debug.Log( text1.text + text2.text + text3.text + text4.text + text5.text + text6.text );
            men.addSCore();
            men.currentQuestion++;
            disable.SetActive(false);
            
        }
        else {va.WrongAnswer(targetTransform);
        men.minusScor();
        Debug.Log( text1.text + text2.text + text3.text + text4.text + text5.text + text6.text );}


    }
    
}
