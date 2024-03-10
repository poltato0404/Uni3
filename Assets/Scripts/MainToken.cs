using UnityEngine;
using System.Collections;

public class MainToken : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    GameObject gameControl;

    public Sprite[] faces;
    public Sprite back;
    public int faceIndex;
    public bool matched = false;

    public void OnMouseDown()
    {
        if (matched == false)
        {
            if (spriteRenderer.sprite == back)
            {
                if (gameControl.GetComponent<GameControl>().TwoCardsUp() == false)
                {
                    spriteRenderer.sprite = faces[faceIndex];
                    gameControl.GetComponent<GameControl>().AddVisibleFace(faceIndex);
                    matched = gameControl.GetComponent<GameControl>().CheckMatch(faceIndex);

                    if (!matched)
                    {
                        StartCoroutine(FlipBackAfterDelay());
                    }
                }
            }
            else
            {
                spriteRenderer.sprite = back;
                gameControl.GetComponent<GameControl>().RemoveVisibleFace(this.faceIndex);
            }
        }
    }

    private IEnumerator FlipBackAfterDelay()
    {
        yield return new WaitForSeconds(1.0f); // Adjust the delay duration as needed

        // Flip the card back to the original state
        spriteRenderer.sprite = back;
        gameControl.GetComponent<GameControl>().RemoveVisibleFace(this.faceIndex);
    }

    void Awake()
    {
        gameControl = GameObject.Find("GameControl");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
