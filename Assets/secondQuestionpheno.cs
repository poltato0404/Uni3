using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class secondQuestionpheno : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string text1,text2;

    public void toggle()
    {
        if (text.text == text1){ text.text = text2;}
        else{ text.text = text1;}
    }
}
