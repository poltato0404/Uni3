using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class promptManager : MonoBehaviour, IDataPersistence
{
    bool isLaptopRetrieved;
    bool isCoinRetrieved;
    bool isDocumentRetrieved;

    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private TextMeshProUGUI instructionTitle;
    [SerializeField] private GameObject Panel;
    public List<string> evidenceStringList;

    private AudioSource audioSource;
    private Dictionary<string, AudioClip> audioClips;

    void Start()
    {
        Time.timeScale = 1f;
        audioSource = gameObject.AddComponent<AudioSource>();
        LoadAudioClips();
    }

    private void LoadAudioClips()
    {
        audioClips = new Dictionary<string, AudioClip>
        {
            { "laptop", Resources.Load<AudioClip>("Audio/Prompts/laptop") },
            { "coin", Resources.Load<AudioClip>("Audio/Prompts/coin") },
            { "document", Resources.Load<AudioClip>("Audio/Prompts/document") }
        };
    }

    private void PlayAudio(string key)
    {
        if (audioClips.ContainsKey(key))
        {
            audioSource.clip = audioClips[key];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Audio clip not found for key: " + key);
        }
    }

    public void promptLaptop()
    {
        if (!isLaptopRetrieved)
        {
            Panel.SetActive(true);
            instructionTitle.text = "Item Found";
            instructionText.text = "You've found Dr. Doe's laptop! This is key for accessing the minigames. \n After finding all 3 passwords, click the laptop in your inventory to use them when you're ready.";
            PlayAudio("laptop");
        }
    }

    public void promptCoin()
    {
        if (!isCoinRetrieved)
        {
            Panel.SetActive(true);
            instructionTitle.text = "Item Found";
            instructionText.text = "You found a coin! Collect these to upgrade your equipment. Keep exploring to find more.";
            isCoinRetrieved = true;
            PlayAudio("coin");
        }
    }

    public void promptDocument(int y)
    {
        Panel.SetActive(true);
        instructionTitle.text = "Item Found";
        instructionText.text = "You've found a document with a password! \n" + evidenceStringList[y] + "\n Remember and collect all 3 to unlock the 3 minigames on the laptop. Keep searching.";
        isDocumentRetrieved = true;
        PlayAudio("document");
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
}
