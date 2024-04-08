using System.Collections;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] StaminaBar _staminaBar;
    [SerializeField] playerControScript _playerContro;
    [SerializeField] GameObject sprintButton; // Reference to the sprint button GameObject

    float _playerOriginalSpeed;
    float _playerSprintSpeed;
    bool isSprinting = false;
    bool isRegenerationDelayed = false; // Flag to indicate if regeneration is delayed
    float regenerationDelayDuration = 2f; // Duration of delay in seconds

    void Start()
    {
        _playerOriginalSpeed = _playerContro.playerSpeed;
        _playerSprintSpeed = _playerContro.playerSpeed * 2f;
    }

    void Update()
    {
        // If the player is sprinting, use stamina and set sprinting speed
        if (isSprinting)
        {
            if (StaminaGameManager.staminaGameManager._playertStamina.Stamina > 0)
            {
                PlayerUseStamina(60f);
                _playerContro.playerSpeed = _playerSprintSpeed;
            }
            else
            {
                StopSprinting(); // Stop sprinting if stamina runs out
                _playerContro.playerSpeed = _playerOriginalSpeed; // Reset speed to original
                isRegenerationDelayed = true; // Set flag to delay regeneration
                StartCoroutine(DelayedRegeneration()); // Start coroutine for delayed regeneration
            }
        }
        else
        {
            if (!isRegenerationDelayed)
            {
                PlayerRegenStamina(); // Regular stamina regeneration
            }
        }
    }

    IEnumerator DelayedRegeneration()
    {
        yield return new WaitForSeconds(regenerationDelayDuration);
        isRegenerationDelayed = false; // Reset the flag
        PlayerRegenStamina(); // Resume stamina regeneration
    }

    // Method called when the player presses the sprint button
    public void StartSprinting()
    {
        isSprinting = true;
    }

    // Method called when the player releases the sprint button or when stamina runs out
    public void StopSprinting()
    {
        isSprinting = false;
    }

    private void PlayerUseStamina(float staminaAmount)
    {
        StaminaGameManager.staminaGameManager._playertStamina.UseStamina(staminaAmount);
        _staminaBar.SetStamina(StaminaGameManager.staminaGameManager._playertStamina.Stamina);
    }

    private void PlayerRegenStamina()
    {
        StaminaGameManager.staminaGameManager._playertStamina.RegenStamina();
        _staminaBar.SetStamina(StaminaGameManager.staminaGameManager._playertStamina.Stamina);
    }
}