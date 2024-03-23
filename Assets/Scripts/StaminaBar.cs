using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{

    public Image staminaBar;

    public float Stamina, MaxStamina;

    public float AttackCost;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            Debug.Log("Attack!");

            Stamina -= AttackCost;
            if (Stamina < 0) Stamina = 0;
            staminaBar.fillAmount = Stamina / MaxStamina;
        }
    }
}
