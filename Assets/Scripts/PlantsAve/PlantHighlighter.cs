using UnityEngine;

public class PlantHighlighter : MonoBehaviour
{
    private Material originalMaterial;
    public Material highlightMaterial;

    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
    }

    public void HighlightPlant()
    {
        GetComponent<Renderer>().material = highlightMaterial;
    }

    public void ResetHighlight()
    {
        GetComponent<Renderer>().material = originalMaterial;
    }
}