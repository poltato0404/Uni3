using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Type
{
    Bomb,
    Mole
}

public class MolePop : MonoBehaviour
{
    public Type type;

    public string answer;
    public bool isHit = false;

    public TextMeshProUGUI text;
    [Range(0f, 1f)] public float randomTextChance = 0.4f;

    public string[] randomText = { "Transformation", "Cell Merging", "Cell Division", "Tranposition", "DNA Splicing" };

    private void Start()
    {
        if (type == Type.Mole)
        {
            float randomValue = Random.value;
            if (randomValue < randomTextChance)
            {
                int random = Random.Range(0, randomText.Length);
                text.text = randomText[random];
                answer = randomText[random];
            }
        }
    }


    public bool Hit()
    {
        isHit = true;
        return isHit;
    }

    public void OnMouseDown()
    {
        switch (type)
        {
            case Type.Mole:
                ObstacleSpawner.Instance.DestroyMole(answer, this.transform);
                Destroy(this.gameObject, 0.1f);
                break;
            case Type.Bomb:
                //Debug.Log("LOSE");
                ObjectivesRecombinantDNA.Instance.gameWinLose.SetActive(true);
                Destroy(this.gameObject, 0.1f);
                break;
        }
        
    }

    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }
}