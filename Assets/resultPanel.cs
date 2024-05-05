using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class resultPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI raisedText;
    [SerializeField] private TextMeshProUGUI flatText;
    [SerializeField] private TextMeshProUGUI whiteText;
    [SerializeField] private TextMeshProUGUI blackText;
    [SerializeField] private TextMeshProUGUI largeText;
    [SerializeField] private TextMeshProUGUI smallText;
    public void displayResult(ChickenTraits chick1, ChickenTraits chick2)
    {
        if(chick1.isBlack == true || chick2.isBlack == true)
        {
            blackText.text = ":  100%";
            whiteText.text = ":  0%";
        }
        else {
            whiteText.text = ":  100%";
            blackText.text  = ":  0%";
        }

        if(chick1.isLarge == false || chick2.isLarge == false)
        {
            smallText.text = ":  100%";
            largeText.text = ":  0%";
        }
        else {
            largeText.text = ":  100%";
            smallText.text  = ":  0%";
        }

        if(chick1.isRaised == false || chick2.isRaised == false)
        {
            raisedText.text = ":  0%";
            flatText.text = ":  100%";
        }
        else {
            raisedText.text = ":  100%";
            flatText.text = ":  0%";
        }


        
    }
}
