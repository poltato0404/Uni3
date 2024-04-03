using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class animationScript : MonoBehaviour
{

    Animator characterAnimator;
    private bool walk = false;
    [SerializeField] playerControScript walking;
     AudioSource footsteps;

        

    // Start is called before the first frame update
    void Start()
    {
        footsteps = GetComponent<AudioSource>();
        characterAnimator = GetComponent<Animator>();  
    }

    void Update()
    {
        
        walk = walking.isWalking;
        if (walk == true)
        {
            if(!footsteps.isPlaying){
                footsteps.Play();
            }
            
            characterAnimator.SetBool("isWalking", true);
        }
        else 
        {
            
           
            characterAnimator.SetBool("isWalking", false);
        }


    }
    

}
