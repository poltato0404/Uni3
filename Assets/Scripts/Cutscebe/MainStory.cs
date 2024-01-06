using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainStory : MonoBehaviour
{

    void OnEnable() {
// yung nakaspecify lang ata yung maglload  with the single mode
    SceneManager.LoadScene("main", LoadScene.Single);  }

}

  