using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateDestroy : MonoBehaviour
{
    public void Start()
    {

        StartCoroutine(WaitAndPrint());
    }

    // Coroutine to wait for 1 second
    IEnumerator WaitAndPrint()
    {
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}