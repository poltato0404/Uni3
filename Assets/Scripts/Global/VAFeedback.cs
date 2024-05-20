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

    public Canvas uiCanvas;

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
        InstantiateFeedback(rightAnswerPrefab, position);
    }

    public void WrongAnswer(Transform position)
    {
        source.PlayOneShot(clips[1]);
        InstantiateFeedback(wrongAnswerPrefab, position);
    }

    private void InstantiateFeedback(GameObject prefab, Transform position)
    {
        GameObject obj = Instantiate(prefab, uiCanvas.transform); // Instantiate as a child of the UI Canvas

        RectTransform rectTransform = obj.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, position.position);
            rectTransform.anchoredPosition = screenPoint - uiCanvas.GetComponent<RectTransform>().sizeDelta / 2f;

            // Align rotation with the camera if needed (usually not required for 2D UI elements)
            // obj.transform.rotation = Camera.main.transform.rotation;
        }
    }
}
