using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;

public class EndSceneSubtitle : MonoBehaviour
{
    public TMP_Text subtitleText;
    public string[] subtitles;
    public float[] startTimes;
    public float[] endTimes;
    [SerializeField] GameObject textSubt;
    [SerializeField] Image background; // Reference to the background image
    public VideoPlayer videoPlayer;

    private bool subtitlesEnabled = true;

    private void Start()
    {
        // Populate subtitle data
        subtitles = new string[]
        {
            "After months of searching, tonight, everything ends here.",
            "Big news! Dr. Smith is free, and Dr. Doe is in trouble for doing the wrong thing.",
            "It is with great honor that we present the Nobel Prize in Biology to Dr. Smith for his groundbreaking research and unwavering commitment to truth and justice.",
            "Son, I always believed that the truth would prevail",
            "Since you were young, I always knew you'd become a great detective instead of a scientist.",
            "Thank you for never giving up on me.",
            "I'm just glad you're home, Dad.",
            "We make a great team, don't we?"
        };

        // Define start and end times for each subtitle line
        startTimes = new float[]
        {
            0f,
            6f,
            12f,
            18f,
            26f,
            31f,
            36f,
            41f,
            43f
        };

        endTimes = new float[]
        {
            4f,
            12f,
            24f,
            27f,
            30f,
            35f,
            38f,
            42f,
            44f
        };
    }

    private void Update()
    {
        if (!subtitlesEnabled)
        {
            // If subtitles are disabled, clear the subtitle text and hide the background
            subtitleText.text = "";
            background.enabled = false;
            return;
        }

        double currentTime = videoPlayer.time;

        // Check if current time falls within subtitle time range
        for (int i = 0; i < subtitles.Length; i++)
        {
            if (currentTime >= (double)startTimes[i] && currentTime <= (double)endTimes[i])
            {
                subtitleText.text = subtitles[i]; // Display subtitle text
                background.enabled = true; // Show the background
                return;
            }
        }

        // If no subtitle is being displayed, clear the subtitle text and hide the background
        subtitleText.text = "";
        background.enabled = false;
    }

    public void EnableSubtitles()
    {
        subtitlesEnabled = true;
    }

    public void DisableSubtitles()
    {
        subtitlesEnabled = false;
        subtitleText.text = "";
        background.enabled = false; // Hide the background when subtitles are disabled
        textSubt.SetActive(false);
    }

    public void SaveData(ref GameData data)
    {

    }
    public void LoadData(GameData data)
    {
        if (data.subtitle)
        {
            EnableSubtitles();
        }
        else
        {
            DisableSubtitles();
            textSubt.SetActive(false);
        }
    }
}
