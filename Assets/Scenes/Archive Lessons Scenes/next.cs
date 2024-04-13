using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] videoClips;
    private int currentClipIndex = 0;

    void Start()
    {
        videoPlayer.clip = videoClips[currentClipIndex];
        videoPlayer.Play();
    }

    public void PlayNextVideo()
    {
        currentClipIndex++;
        if (currentClipIndex < videoClips.Length)
        {
            videoPlayer.Stop();
            videoPlayer.clip = videoClips[currentClipIndex];
            videoPlayer.Play();
        }
        else
        {
            Debug.Log("No more videos");
        }
    }
}
