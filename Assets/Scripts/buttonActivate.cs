using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonActivate : MonoBehaviour
{
    public LevelManager  levman;
    [SerializeField] private GameObject changeToMinigame;

    void Start()
    {
        changeToMinigame.SetActive(false);
    }

    // OnTriggerExit is called when another collider exits this collider
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag ==  "memory")
        {
            changeToMinigame.SetActive(false);
        }
         if (other.gameObject.tag ==  "slide" )
        {
            changeToMinigame.SetActive(false);
        }
         if (other.gameObject.tag ==  "dragndrop")
        {
            changeToMinigame.SetActive(false);
        }
    }

    // OnTriggerEnter is called when another collider enters this collider
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "dragndrop")
        {
            changeToMinigame.SetActive(true);
            levman.changeToDrag();

        }
        if (other.gameObject.tag == "slide")
        {
            changeToMinigame.SetActive(true);
            levman.changeToSlid();
        }
        if (other.gameObject.tag == "memory")
        {
            changeToMinigame.SetActive(true);
            levman.changeToMem();
        }
    }
}
