using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameControllerScript : MonoBehaviour
{
    public const int columns = 4;
    public const int rows = 2;

    public const float Xspace = 4f;

    [SerializeField] private MainImageScript startObject;
    [SerializeField] private Sprite[] images;

    private void Start()
    {
        int[] locations = { 0, 0, 1, 1, 2, 2, 3, 3 };
        locations = Randomiser(locations);

        float cardHeight = startObject.GetComponent<SpriteRenderer>().bounds.size.y;
        float separationFactor = 1.5f; // Adjust this factor as needed

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                float positionY = j * (cardHeight + separationFactor);

                MainImageScript gameImage = Instantiate(startObject, new Vector3(i * Xspace, positionY, i), Quaternion.identity);

                int index = j * columns + i;
                int id = locations[index];
                gameImage.ChangeSprite(id, images[id]);

                // Set the sorting layer
                gameImage.GetComponent<SpriteRenderer>().sortingLayerName = "YourSortingLayerName";

                // Debug statement for cloned objects
                Debug.Log($"Cloned object at {gameImage.transform.position} with sprite ID {id}");
            }
        }
    }

    private int[] Randomiser(int[] locations)
    {
        int[] array = locations.Clone() as int[];
        for (int i = 0; i < array.Length; i++)
        {
            int newArray = array[i];
            int j = Random.Range(i, array.Length);
            array[i] = array[j];
            array[j] = newArray;
        }
        return array;
    }

    private MainImageScript firstOpen;
    private MainImageScript secondOpen;

    private int score = 0;
    private int attempts = 0;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI attemptsText;

    public bool canOpen
    {
        get { return secondOpen == null; }
    }

    public void imageOpened(MainImageScript startObject)
    {
        if (firstOpen == null)
        {
            firstOpen = startObject;
        }
        else
        {
            secondOpen = startObject;
            StartCoroutine(CheckGuessed());
        }
    }

    private IEnumerator CheckGuessed()
    {
        if (firstOpen.SpriteId == secondOpen.SpriteId)
        {
            score++;
            scoreText.SetText("Score: " + score);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            firstOpen.Close();
            secondOpen.Close();
        }

        attempts++;
        attemptsText.SetText("Attempts: " + attempts);

        firstOpen = null;
        secondOpen = null;
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }
}
