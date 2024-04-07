using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] StaminaBar _staminaBar;
    [SerializeField] playerControScript _playerContro;

    float _playerOriginalSpeed;
    float _playerSprintSpeed;

    void Start()
    {
        _playerOriginalSpeed = _playerContro.playerSpeed;
        _playerSprintSpeed = _playerContro.playerSpeed * 2f;
    }

    void Update()
    {
        //Stamina
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (StaminaGameManager.staminaGameManager._playertStamina.Stamina > 0)
            {
                PlayerUseStamina(60f);
                if (_playerContro.playerSpeed != _playerSprintSpeed)
                {
                    _playerContro.playerSpeed = _playerSprintSpeed;
                }
            }
            else
            {
                _playerContro.playerSpeed = _playerOriginalSpeed;

            }
        }
        else
        {
            PlayerRegenStamina();
            if (_playerContro.playerSpeed != _playerOriginalSpeed)
            {
                _playerContro.playerSpeed = _playerOriginalSpeed;
            }
        }
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
