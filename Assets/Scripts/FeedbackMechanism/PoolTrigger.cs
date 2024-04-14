using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTrigger : MonoBehaviour
{
    public Transform point;
    public int score;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = point.position;
            QuestManager.Instance.UpdateScore(-score);
        }
    }
}