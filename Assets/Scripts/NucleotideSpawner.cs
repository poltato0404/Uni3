using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NucleotideSpawner : MonoBehaviour
{
    public bool isRunning = true;

    public float spawnDelay = 3.5f;

    [Range(0f, 1f)]
    public float spawnChance = 0.3f;

    public GameObject nucleotidePrefab;

    private void Start()
    {
        StartCoroutine(gameLoop());
    }

    IEnumerator gameLoop()
    {
        while (isRunning)
        {
            float random = Random.value;
            if (spawnChance < random)
            {
                GameObject instantiatedObj = Instantiate(nucleotidePrefab, this.transform.position, Quaternion.Euler(
                    Random.Range(-45f, 45f),
                    Random.Range(-45f, 45f),
                    Random.Range(-45, 45f)));

                instantiatedObj.transform.parent = this.transform;
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}