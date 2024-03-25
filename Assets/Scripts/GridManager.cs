using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridSizeX = 5;
    public int gridSizeY = 5;
    public float spacing = 1.2f;

    public GameObject cubePrefab;

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 spawnPosition = new Vector3(x * spacing, 0, y * spacing);
                Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
