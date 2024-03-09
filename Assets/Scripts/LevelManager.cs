using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void changeToDrag(){
        sceneName = "Drag&DropMiniGame";
    }
     public void changeToMem(){
        sceneName = "MemoryGame";
    }
     public void changeToSlid(){
        sceneName = "slide";
    }
}
