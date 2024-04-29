using UnityEngine;

public class CubeDestroyer1 : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Nucleotide"))
        {
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("Polypeptide"))
        {
            other.transform.position = new Vector3(-5.75f, 8.68999958f, 1.42274725f);
        }
    }
}