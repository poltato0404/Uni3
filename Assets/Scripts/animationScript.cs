using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class animationScript : MonoBehaviour
{

    Animator characterAnimator;
    private bool walk = false;
    [SerializeField] playerControScript walking;

    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = GetComponent<Animator>();  
    }

    void Update()
    {
        
        walk = walking.isWalking;
        if (walk == true)
        {
            Debug.Log("walking");
            characterAnimator.SetBool("isWalking", true);
        }
        else 
        {
            Debug.Log("not walking");
            characterAnimator.SetBool("isWalking", false);
        }

    }
}
