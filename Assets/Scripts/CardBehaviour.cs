using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    [SerializeField] private float lerpSpeed;

    public int matchID;

    public void OnMouseDown()
    {
        if (CardHolder.instance.CheckMatching())
        {
            StartCoroutine(FlipObject());
            CardHolder.instance.SetCard(this);
        }
    }

    public void FlipBack()
    {
        StartCoroutine(FlipObject());
    }


    IEnumerator FlipObject()
    {
        float timeElapsed = 0f;

        Vector3 bobUp = new Vector3(transform.position.x, 2, transform.position.z);
        Vector3 bobDown = new Vector3(transform.position.x, 1, transform.position.z);

        while(timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * lerpSpeed;
            transform.position = Vector3.Lerp(bobDown, bobUp, timeElapsed);
            yield return null;
        }

        //yield return new WaitForSeconds(0.1f);

        timeElapsed = 0f;

        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * lerpSpeed;
            transform.position = Vector3.Lerp(bobUp, bobDown, timeElapsed);
            yield return null;
        }


        if (transform.rotation == Quaternion.Euler(0, 0, 180))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }
}