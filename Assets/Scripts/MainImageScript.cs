using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainImageScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject image_back;
    [SerializeField] private GameControllerScript gameController;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (image_back.activeSelf && gameController.canOpen)
        {
            image_back.SetActive(false);
            gameController.imageOpened(this);
        }
    }

    private int _spriteId;
    public int SpriteId
    {
        get { return _spriteId; }
    }

    public void ChangeSprite(int id, Sprite image)
    {
        _spriteId = id;
        GetComponent<SpriteRenderer>().sprite = image;
        Debug.Log($"Changed sprite for card {_spriteId}");
    }

    public void Close()
    {
        image_back.SetActive(true);
    }
}
