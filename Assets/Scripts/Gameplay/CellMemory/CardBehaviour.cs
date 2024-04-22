using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    [SerializeField] private float lerpSpeed;

    public int matchID;
    public int cardNumber;

    public bool canFlipThisCard = true;

    public void OnMouseDown()
    {
        if (CardHolder.instance.CheckMatching() && canFlipThisCard)
        {
            StartCoroutine(FlipCard());
            CardHolder.instance.SetCard(this);
        }
    }

    public void FlipBack()
    {
        StartCoroutine(FlipBackToInitial());
    }

    IEnumerator FlipCard()
    {
        float timeElapsed = 0f;

        Vector3 bobUp = new Vector3(transform.position.x, 2, transform.position.z);
        Vector3 bobDown = new Vector3(transform.position.x, 1, transform.position.z);

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, 180); // or Quaternion.Euler(0, 0, 0) depending on your initial state

        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * lerpSpeed;
            transform.position = Vector3.Lerp(bobDown, bobUp, timeElapsed);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, timeElapsed);
            yield return null;
        }

        //yield return new WaitForSeconds(0.1f);

        timeElapsed = 0f;

        // Reverse the rotation targets
        targetRotation = Quaternion.Euler(0, 0, 0); // or Quaternion.Euler(0, 0, 180) depending on your initial state

        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * lerpSpeed;
            transform.position = Vector3.Lerp(bobUp, bobDown, timeElapsed);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, timeElapsed);
            yield return null;
        }

        // Ensure the rotation is exactly the target rotation
        transform.rotation = targetRotation;
    }

    private IEnumerator FlipBackToInitial()
    {
        float timeElapsed = 0f;
        Vector3 bobUp = new Vector3(transform.position.x, 2, transform.position.z);
        Vector3 bobDown = new Vector3(transform.position.x, 1, transform.position.z);

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, 180); // or Quaternion.Euler(0, 0, 0) depending on your initial state

        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * lerpSpeed;
            transform.position = Vector3.Lerp(bobDown, bobUp, timeElapsed);
            yield return null;
        }

        timeElapsed = 0f;

        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * 1.5f; // Slower lerpSpeed
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, timeElapsed);
            yield return null;
        }

        timeElapsed = 0f;

        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * lerpSpeed;
            transform.position = Vector3.Lerp(bobUp, bobDown, timeElapsed);
            yield return null;
        }

        // Ensure the rotation is exactly the target rotation
        transform.rotation = targetRotation;
        canFlipThisCard = true;
    }
}