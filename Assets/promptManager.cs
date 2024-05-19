using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class promptManager : MonoBehaviour, IDataPersistence
{
    bool isLaptopRetrieved;
    bool isCoinRetrieved;
    bool isDocumentRetrieved;

    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private TextMeshProUGUI instructionTitle;
    [SerializeField] private GameObject Panel;
    [SerializeField] private Button exitButton;
    public List<string> evidenceStringList;

    private AudioSource promptAudioSource;
    private Dictionary<string, AudioClip> audioClips;

    [SerializeField] private AudioSource backgroundMusicAudioSource;
    private float originalMusicVolume;

    void Start()
    {
        Time.timeScale = 1f;
        promptAudioSource = gameObject.AddComponent<AudioSource>();
        originalMusicVolume = backgroundMusicAudioSource.volume;
        LoadAudioClips();

        // Ensure the exit button stops audio and closes the panel
        exitButton.onClick.AddListener(StopAudioAndClosePanel);
    }

    private void LoadAudioClips()
    {
        audioClips = new Dictionary<string, AudioClip>
        {
            { "laptop", Resources.Load<AudioClip>("Audio/Prompts/laptop") },
            { "coin", Resources.Load<AudioClip>("Audio/Prompts/coin") },
            { "document", Resources.Load<AudioClip>("Audio/Prompts/document") }
        };

        foreach (var clip in audioClips)
        {
            if (clip.Value == null)
            {
                Debug.LogError("Audio clip for " + clip.Key + " not found!");
            }
        }
    }

    private IEnumerator PlayAudio(string key)
    {
        if (audioClips.ContainsKey(key) && audioClips[key] != null)
        {
            // Lower the background music volume
            backgroundMusicAudioSource.volume = originalMusicVolume * 0.3f;

            promptAudioSource.clip = audioClips[key];
            promptAudioSource.Play();

            // Wait until the prompt audio finishes playing
            yield return new WaitForSeconds(promptAudioSource.clip.length);

            // Restore the background music volume
            backgroundMusicAudioSource.volume = originalMusicVolume;
        }
        else
        {
            Debug.LogWarning("Audio clip not found or is null for key: " + key);
        }
    }

    public void promptLaptop()
    {
        if (!isLaptopRetrieved)
        {
            Debug.Log("Displaying laptop prompt");
            Panel.SetActive(true);
            instructionTitle.text = "Item Found";
            instructionText.text = "You've found Dr. Doe's laptop! This is key for accessing the minigames. \n After finding all 3 passwords, click the laptop in your inventory to use them when you're ready.";
            StartCoroutine(PlayAudio("laptop"));
        }
    }

    public void promptCoin()
    {
        if (!isCoinRetrieved)
        {
            Debug.Log("Displaying coin prompt");
            Panel.SetActive(true);
            instructionTitle.text = "Item Found";
            instructionText.text = "You found a coin! Collect these to upgrade your equipment. Keep exploring to find more.";
            isCoinRetrieved = true;
            StartCoroutine(PlayAudio("coin"));
        }
        else
        {
            Debug.Log("Coin already retrieved");
        }
    }

    public void promptDocument(int y)
    {
        Debug.Log("Displaying document prompt");
        Panel.SetActive(true);
        instructionTitle.text = "Item Found";
        instructionText.text = "You've found a document with a password! \n" + evidenceStringList[y] + "\n Remember and collect all 3 to unlock the 3 minigames on the laptop. Keep searching.";
        isDocumentRetrieved = true;
        StartCoroutine(PlayAudio("document"));
    }

    public void SaveData(ref GameData data)
    {
        data.isDocumentRetrieved = isDocumentRetrieved;
        data.isCoinRetrieved = isCoinRetrieved;
    }

    public void LoadData(GameData data)
    {
        isDocumentRetrieved = data.isDocumentRetrieved;
        isCoinRetrieved = data.isCoinRetrieved;
        isLaptopRetrieved = data.isLaptopRetrieved;
    }

    public void StopAudioAndClosePanel()
    {
        // Stop the audio
        if (promptAudioSource.isPlaying)
        {
            promptAudioSource.Stop();
        }

        // Restore the background music volume
        backgroundMusicAudioSource.volume = originalMusicVolume;

        // Hide the panel
        Panel.SetActive(false);
    }
}
