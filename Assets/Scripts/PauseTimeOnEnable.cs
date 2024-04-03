using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTimeOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0.0f;
    }


    public void OnClick_OkButton()
    {
        Time.timeScale = 1.0f;
    }
}