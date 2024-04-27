using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gotoFinish : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        // Start the coroutine
        StartCoroutine(WaitAndExecute());
    }

    IEnumerator WaitAndExecute()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(4f);
        player.SetActive(false);
        // Code to execute after the wait
         SceneManager.LoadScene("Shop");
        
        // Additional code to execute after the delay
        // For example, enable an object, trigger an event, etc.
    }
}
