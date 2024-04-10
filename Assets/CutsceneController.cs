using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName; // Name of the next scene

    void Start()
    {
        videoPlayer.loopPointReached += OnCutsceneEnd;
    }

    void OnCutsceneEnd(VideoPlayer vp)
    {
        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
