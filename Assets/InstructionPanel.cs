using UnityEngine;
using TMPro;

public class TapToContinue : MonoBehaviour
{
    // Reference to the TextMeshPro object
    public TMP_Text tapToContinueText;
    // Reference to the panel
    public GameObject panel;

    // Reference to the AudioSource for instruction audio
    private AudioSource instructionAudioSource;
    private AudioClip instructionAudioClip;

    // Reference to the AudioSource for background music
    [SerializeField] private AudioSource backgroundMusicAudioSource;
    private float originalMusicVolume;

    // Flag to track if the instruction audio is playing
    private bool isInstructionAudioPlaying = false;

    void Start()
    {
        // Get the AudioSource component for instruction audio
        instructionAudioSource = gameObject.AddComponent<AudioSource>();

        // Save the original volume of the background music
        originalMusicVolume = backgroundMusicAudioSource.volume;

        // Stop the background music
        backgroundMusicAudioSource.Stop();

        // Load the instruction audio clip from Resources
        instructionAudioClip = Resources.Load<AudioClip>("Audio/Prompts/genInstruction");

        // Check if the audio clip is loaded successfully
        if (instructionAudioClip == null)
        {
            Debug.LogError("Instruction audio clip not found in Resources/Audio/Prompts");
        }
        else
        {
            // Play the instruction audio clip
            PlayInstructionAudio();
        }
    }

    void Update()
    {
        // Check if the player taps the screen (for touch devices)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            // Hide the "Tap to continue" text and the panel
            tapToContinueText.gameObject.SetActive(false);
            panel.SetActive(false);
            // Stop the instruction audio and start the background music
            StopInstructionAudio();
            StartBackgroundMusic();
        }
        // Check if the player clicks the mouse button (for non-touch devices)
        else if (Input.GetMouseButtonDown(0))
        {
            // Hide the "Tap to continue" text and the panel
            tapToContinueText.gameObject.SetActive(false);
            panel.SetActive(false);
            // Stop the instruction audio and start the background music
            StopInstructionAudio();
            StartBackgroundMusic();
        }
    }

    private void PlayInstructionAudio()
    {
        // Play the audio clip if it's loaded
        if (instructionAudioClip != null)
        {
            instructionAudioSource.clip = instructionAudioClip;
            instructionAudioSource.Play();
            isInstructionAudioPlaying = true;
        }
    }

    private void StopInstructionAudio()
    {
        // Stop the instruction audio if it's playing
        if (isInstructionAudioPlaying)
        {
            instructionAudioSource.Stop();
            isInstructionAudioPlaying = false;
        }
    }

    private void StartBackgroundMusic()
    {
        // Start playing the background music if it's not already playing
        if (!backgroundMusicAudioSource.isPlaying)
        {
            backgroundMusicAudioSource.Play();
        }
    }
}
