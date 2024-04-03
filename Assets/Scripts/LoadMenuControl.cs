using GameEssentials.GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// public Slider progressBar;

public class LoadMenuControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAndSwitchScenes(GameManager.Instance.sceneToLoad));
        // Prepare
    }

    IEnumerator LoadAndSwitchScenes(int sceneToLoad)
    {
        yield return new WaitForSeconds(Random.Range(1, 3));

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        // sceneLoaded = true;
    }
}