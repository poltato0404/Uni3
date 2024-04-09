using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextSceneLoader : MonoBehaviour
{
    public Slider progressBar;
    public float delayBeforeLoading = 1f; // Delay in seconds

    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(delayBeforeLoading);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneIndex);

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            progressBar.value = progress;
            yield return null;
        }
    }
}
