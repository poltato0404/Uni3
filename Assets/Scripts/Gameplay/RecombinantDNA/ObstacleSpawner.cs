using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleSpawner1 : MonoBehaviour
{
    public Transform spawnPoint;
    public List<GameObject> obstacles;

    [Range(0, 15f)] public float spawnDelay = 2f;

    [Range(0f, 1f)] public float spawnChance = 0.5f;

    public Transform lookPoint;

    public static ObstacleSpawner1 Instance;

    private void Awake1 ()
    {
        if(Instance == null)
            Instance = this;
    }


    public void Start1()
    {
        StartCoroutine(SpawnLoop1());
    }

    /*
    private void FixedUpdate()
    {
        if(!isRunning) StartCoroutine(SpawnLoop());
    }
    */

    IEnumerator SpawnLoop1()
    {
        while (true)
        {
            float randomValue = Random.value;

            if (randomValue < spawnChance)
            {
                int randomIndex = Random.Range(0, obstacles.Count);
                GameObject instantiatedObject = Instantiate(obstacles[randomIndex], spawnPoint.position, Quaternion.identity);

                instantiatedObject.transform.LookAt(lookPoint);
                instantiatedObject.transform.parent = this.gameObject.transform;
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void DestroyMole1 (string answer, Transform transform)
    {
        ObjectivesRecombinantDNA.Instance.SubmitAnswer(answer, transform);
    }
}