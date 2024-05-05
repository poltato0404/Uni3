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
            "My father Dr. Smith is on the verge of receiving a prestigious award.",
            "he's a notable Biologist scientist.",
            "And everything changed when he was framed and imprisoned.",
            "I know who framed him up...",
            "out of jealousy.",
            "It was his colleague",
            "Dr. Doe",
            "Just have to find pieces of evidence here",
            "And stop Dr. Doe from destroying it"
        };

        startTimes = new float[]
        {
            1f,  // Start time of first subtitle
            5f,
            8f,
            11f,
            13f,
            16f,
            18f,
            20f,
            23f
        };

        endTimes = new float[]
        {
            4f,  // End time of first subtitle
            8f,
            11f,
            13f,
            14f,
            17f,
            19f,
            22f,
            25f
        };
    }

    private void Update()
    {
        if (!subtitlesEnabled)
        {
            subtitleText.text = "";
            return;
        }

        double currentTime = videoPlayer.time;

        for (int i = 0; i < subtitles.Length; i++)
        {
            if (currentTime >= (double)startTimes[i] && currentTime <= (double)endTimes[i])
            {
                subtitleText.text = subtitles[i]; // Display subtitle text
                return;
            }
        }

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
