using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class SubtitleManager : MonoBehaviour, IDataPersistence
{
    public TMP_Text subtitleText;
    public string[] subtitles;
    public float[] startTimes;
    public float[] endTimes;
    [SerializeField] GameObject textSubt;
    public VideoPlayer videoPlayer;

    private bool subtitlesEnabled = true;

    private void Start()
    {
        // Populate subtitle data
        subtitles = new string[]
        {
            "Hello?",
            "Hello, Detective.",
            "I think I saw the criminal biologist.",
            "I saw him entering this abandoned building right now!",
            "I really tried to follow him...but",
            "he suddenly disappeared.",
            "You're not going to believe me.",
            "and I am not even sure if I will really believe in myself...",
            "but this building...",
            "is like a labyrinth of mysteries."
        };

        // Define start and end times for each subtitle line
        startTimes = new float[]
        {
            5f,  // Start time of first subtitle
            6f,
            7f,
            11f,
            14f,
            17f,
            21f,
            22f,
            26f,
            29f
        };

        endTimes = new float[]
        {
            6f,  // End time of first subtitle
            7f,
            11f,
            14f,
            17f,
            20f,
            22f,
            25f,
            28f,
            32f
        };
    }

    private void Update()
    {
        if (!subtitlesEnabled)
        {
            // If subtitles are disabled, clear the subtitle text and return
            subtitleText.text = "";
            return;
        }

        double currentTime = videoPlayer.time;

        // Check if current time falls within subtitle time range
        for (int i = 0; i < subtitles.Length; i++)
        {
            if (currentTime >= (double)startTimes[i] && currentTime <= (double)endTimes[i])
            {
                subtitleText.text = subtitles[i]; // Display subtitle text
                return;
            }
        }

        // If no subtitle is being displayed, clear the subtitle text
        subtitleText.text = "";
    }

    public void EnableSubtitles()
    {
        subtitlesEnabled = true;
    }

    public void DisableSubtitles()
    {
        subtitlesEnabled = false;
        subtitleText.text = "";
        textSubt.SetActive(false);
    }

    public void SaveData(ref GameData data) { }
    public void LoadData(GameData data) { if (data.subtitle) { EnableSubtitles(); } else { DisableSubtitles(); textSubt.SetActive(false); } }
}
