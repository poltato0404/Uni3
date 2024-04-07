using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaGameManager : MonoBehaviour
{
    public static StaminaGameManager staminaGameManager { get; private set; }

    public UnitStamina _playertStamina = new UnitStamina(100f, 100f, 30f, false);


    void Awake()
    {
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
