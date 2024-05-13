using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructChicken : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Sprite newSprite;
    public Sprite prevSprite;
    public  Image imageComponent;
    public GameObject nextButton;
    public GameObject prevButton;
    public bool ifNext;


    
    void Start()
    {
        
        Time.timeScale = 0f;
    }
    public void Continue()
    {
        // Set time scale to 0, pausing the game
        Time.timeScale = 1f;
    }

    public void Next(){
         ifNext =  true;
    }
     public void Prev(){
       ifNext =  false;
        
    }
    void Update()
    {
        if(ifNext){
        imageComponent.sprite = newSprite;
        text.text = "Your Goal is to answer the questions by clicking the Question Mark Button below. Goodluck! ";
        nextButton.SetActive(false);
        prevButton.SetActive(true);
        }

        else{
        imageComponent.sprite = prevSprite;
         text.text = "These Chickens have pure Genotypes. Drag Them into the plate \n to know their possible Offspring. Find out which traits are \n dominant and Recessive";
        nextButton.SetActive(true);
        prevButton.SetActive(false);
        }

    }

    
}
