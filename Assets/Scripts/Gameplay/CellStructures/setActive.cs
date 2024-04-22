using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setActive : MonoBehaviour
{
    [SerializeField] GameObject targetObject;

    public void SetActive()
    {
        Vector3 lubog = new Vector3(1.7f, 5.6f, 1.65f);
        targetObject.transform.position = lubog;
    }

    public void Disabled()
    {
        Vector3 litaw = new Vector3(1.7f, -1, 1.65f);
        targetObject.transform.position = litaw;
    }
}
