using UnityEngine;
using UnityEngine.UI;

public class InsLaptop : MonoBehaviour
{
    // Audio clip for prompt
    [SerializeField] public AudioClip instructionPromptClip;

    // Reference to AudioSource
    private AudioSource audioSource;

    // Reference to Exit Button
    [SerializeField] private Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Play the audio prompt at the start
        PlayAudioPrompt();

        // Add listener to the exit button to stop audio when clicked
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(StopAudioPrompt);
        }
        else
        {
            Debug.LogError("Exit button is not assigned.");
        }
    }

    // Method to play audio prompt
    private void PlayAudioPrompt()
    {
        // Check if the audio clip is not null
        if (instructionPromptClip != null)
        {
            // Play the audio clip
            audioSource.clip = instructionPromptClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Instruction audio clip is missing.");
        }
    }

    // Method to stop audio prompt
    public void StopAudioPrompt()
    {
        // Stop the audio prompt if it's playing
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
