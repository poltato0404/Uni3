using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ifrestart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(forceload());
    }

    IEnumerator forceload()
    {

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("level1");
        yield return null;


    }
}
