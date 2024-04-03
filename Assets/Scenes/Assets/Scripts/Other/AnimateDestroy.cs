using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateDestroy : MonoBehaviour
{
    public void Start()
    {
        
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}