using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapScript : MonoBehaviour
{
    public Transform player;
    void LateUpdate()
    
    {

        Vector3 newposition = player.position;
        newposition.y = transform.position.y;
        transform.position = newposition;
    }
}
