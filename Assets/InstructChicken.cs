using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructChicken : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        Time.timeScale = 0f;
    }
    public void Continue()
    {
        // Set time scale to 0, pausing the game
        Time.timeScale = 1f;
    }

    
}
