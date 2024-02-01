using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonActivate : MonoBehaviour
{
    [SerializeField] private GameObject changeToMinigame;

    void Start()
    {
        changeToMinigame.SetActive(false);
    }

    // OnTriggerExit is called when another collider exits this collider
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "device")
        {
            changeToMinigame.SetActive(false);
        }
    }

    // OnTriggerEnter is called when another collider enters this collider
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "device")
        {
            changeToMinigame.SetActive(true);
        }
    }
}
