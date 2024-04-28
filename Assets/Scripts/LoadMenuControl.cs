using GameEssentials.GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// public Slider progressBar;

public class LoadMenuControl : MonoBehaviour
{
    const float MULTIPLIER = 0.01f;
    // Start is called before the first frame update
    public Image loadFill;

    void Start()
    {
        StartCoroutine(LoadAndSwitchScenes(GameManager.Instance.sceneToLoad));
        // Prepare
    }

    IEnumerator LoadAndSwitchScenes(int sceneToLoad)
    {
        loadFill.fillAmount = 10 * MULTIPLIER;

        yield return new WaitForSeconds(Random.Range(1, 3));

        loadFill.fillAmount = 90 * MULTIPLIER;

        yield return new WaitForSeconds(Random.Range(1, 3));

        loadFill.fillAmount = 100 * MULTIPLIER;

        yield return new WaitForSeconds(0.5f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        // sceneLoaded = true;
    }
}