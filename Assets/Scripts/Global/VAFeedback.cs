using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VAFeedback : MonoBehaviour
{
    public static VAFeedback Instance;

    public GameObject rightAnswerPrefab;
    public GameObject wrongAnswerPrefab;

    private AudioSource source;
    public AudioClip[] clips;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void RightAnswer(Transform position)
    {
        source.PlayOneShot(clips[0]);
        GameObject obj = Instantiate(rightAnswerPrefab, position.position, Quaternion.identity);
        obj.GetComponent<Canvas>().worldCamera = Camera.main;

        // Adjust position to be slightly in front of the object
        obj.transform.position += Camera.main.transform.forward * 0.1f;

        // Align rotation with the camera
        obj.transform.rotation = Camera.main.transform.rotation;
    }

    public void WrongAnswer(Transform position)
    {
        source.PlayOneShot(clips[1]);
        GameObject obj = Instantiate(wrongAnswerPrefab, position.position, Quaternion.identity);
        obj.GetComponent<Canvas>().worldCamera = Camera.main;

        // Adjust position to be slightly in front of the object
        obj.transform.position += Camera.main.transform.forward * 0.1f;

        // Align rotation with the camera
        obj.transform.rotation = Camera.main.transform.rotation;
    }
}
