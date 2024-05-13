using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaGameManager : MonoBehaviour
{
    public static StaminaGameManager staminaGameManager { get; private set; }

    public playerControScript pcs;
    public UnitStamina _playertStamina = new UnitStamina(100f, 100f, 30f, false);

    void Update(){
            if (pcs.isWalking)
        {
            _playertStamina.StaminaRegenSpeed = 2.5f;
        }
        else
        {
            _playertStamina.StaminaRegenSpeed = 5f; 
        }
    }

    void Awake()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        pcs = playerObject.GetComponent<playerControScript>();
        if (staminaGameManager != null && staminaGameManager != this)
        {
            Destroy(this);
        }
        else
        {
            staminaGameManager = this;
        }
    }
}
