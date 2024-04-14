using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class archiveVideo : MonoBehaviour
{
    [SerializeField] GameObject video1, video2, video3, video4, video5, video6, video7, video8, video9; 
    [SerializeField] List<GameObject> buttons; 
    // Start is called before the first frame update
    void Start(){
        
    }
    public void playVideo1()
    {
        video1.SetActive(true);
        disableAllButtons();
    }

    public void playVideo2()
    {
        video2.SetActive(true);
        disableAllButtons();
    }

    public void playVideo3()
    {
        video3.SetActive(true);
        disableAllButtons();
    }

    public void playVideo4()
    {
        video4.SetActive(true);
        disableAllButtons();
    }

    public void playVideo5()
    {
        video5.SetActive(true);
        disableAllButtons();
    }

    public void playVideo6()
    {
        video6.SetActive(true);
        disableAllButtons();
    }

      public void playVideo7()
    {
        video7.SetActive(true);
        disableAllButtons();
    }

      public void playVideo8()
    {
        video8.SetActive(true);
        disableAllButtons();
    }

     public void playVideo9()
    {
        video9.SetActive(true);
        disableAllButtons();
    }
    

    void disableAllButtons(){
        for(int i = 0 ; i < buttons.Count; i++ ){
            buttons[i].SetActive(false);
        }
    }
}
