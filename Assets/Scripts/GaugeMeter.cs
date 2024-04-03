using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GaugeMeter : MonoBehaviour
{
    public Image GaugeHand;

    public static GaugeMeter instance;

    private float elapsedTime;

    [SerializeField] private Quaternion initialRotation;
    [SerializeField] private Quaternion targetRotation;

    private bool isRotating = false;
    [SerializeField] private float rotationSpeed = 90f;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        initialRotation = GaugeHand.rectTransform.rotation;
    }

    [ContextMenu("RotateToLeft")]
    public void RotateToLeft()
    {
        // Rotate to Left (Workaround since we can't graph real time heat without implementing an intensive heat map system)

        if (!isRotating)
        {
            targetRotation = Quaternion.Euler(0, 0, 90f);
            StartCoroutine(RotateHand());
        }
    }

    [ContextMenu("RotateToRight")]
    public void RotateToRight()
    {
        if (!isRotating)
        {
            targetRotation = Quaternion.Euler(0, 0, -90f);
            StartCoroutine(RotateHand());
        }
    }

    public void RotateToCenter()
    {
        if (!isRotating)
        {
            targetRotation = Quaternion.Euler(0, 0, 0f);
            StartCoroutine(RotateHand());
        }
    }

    // Just In case
    /// <summary>
    /// Free form rotation so you can indicate which direction to rotate (from z = -90f to z = 90f)
    /// Use RotateToAny(Quaternion.Euler(0, 0, your_rotation));
    /// </summary>
    /// <param name="rotation">Will only read the z rotation</param>
    public void RotateToAny(Quaternion rotation)
    {
        if (!isRotating)
        {
            targetRotation = Quaternion.Euler(0, 0, rotation.z);
            StartCoroutine(RotateHand());
        }
    }

    IEnumerator RotateHand()
    {
        isRotating = true;
        float elapsedTime = 0f;
        Quaternion startRotation = GaugeHand.rectTransform.rotation;

        while(elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * rotationSpeed;
            GaugeHand.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
            yield return null;
        }

        isRotating = false;
    }
}