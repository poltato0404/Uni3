using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public List<GameObject> obstacles;

    [Range(0, 15f)] public float spawnDelay = 2f;

    [Range(0f, 1f)] public float spawnChance = 0.5f;

    public Transform lookPoint;

    public static ObstacleSpawner Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }


    public void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    /*
    private void FixedUpdate()
    {
        if(!isRunning) StartCoroutine(SpawnLoop());
    }
    */

    IEnumerator SpawnLoop()
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

    public void DestroyMole(string answer, Transform transform)
    {
        ObjectivesRecombinantDNA.Instance.SubmitAnswer(answer, transform);
    }
}