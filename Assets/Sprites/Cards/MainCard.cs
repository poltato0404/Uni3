using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour
{
    [SerializeField] private GameObject Card_Back;

    public void OnMouseDown()
    {
        Card_Back.SetActive(false);
    }
}
