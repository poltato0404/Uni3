using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomize : MonoBehaviour
{
    // Start is called before the first frame update
    private List<int> pathX, pathZ;
    void Start()
    {
        pathX = new List<int>();
        pathZ = new List<int>();
        populate();
        int randomIndex = Random.Range(0, pathX.Count);
        Vector3 randomPosition = new Vector3(pathX[randomIndex], 0, pathZ[randomIndex]);
        transform.position = randomPosition;
    }

    // Update is called once per frame
    void populate()
    {
        int xLength = 6;
        int zLength = 6;
        int origMap = 49;
        int x = -1 * ((xLength * 5) / 2);
        int z = (zLength * 5);


        for (int i = 0; i < origMap; i++)
        {

            pathX.Add(x);
            pathZ.Add(z);

            if (x == (xLength * 5) / 2)
            {
                z = z - 5;
                x = -1 * ((xLength * 5) / 2);
            }
            else
            {
                x = x + 5;
            }

        }
    }

}
