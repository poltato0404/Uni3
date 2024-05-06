using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class goToLoad : MonoBehaviour, IDataPersistence
{
    public GameObject player;
    int currentlevel;
    void startHelo()
    {
        // Start the coroutine
        StartCoroutine(WaitAndExecute());
    }

    IEnumerator WaitAndExecute()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);
        player.SetActive(false);
        // Code to execute after the wait
        if (currentlevel < 3)
        {
            SceneManager.LoadScene("Load");
        }
        else { SceneManager.LoadScene("EndScene"); }



        // Additional code to execute after the delay
        // For example, enable an object, trigger an event, etc.
    }

    public void SaveData(ref GameData data)
    {
        data.currentLevel++;
    }
    public void LoadData(GameData data)
    {
        startHelo();
    }
}
