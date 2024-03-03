using UnityEngine;

public class EquipOrgans : MonoBehaviour
{
    public GameObject Organ;
    public Transform OrganParent;

    void Start()
    {
        Organ.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Drop();
        }
    }

    void Drop()
    {
        Debug.Log("Dropping the organ");

        // Reset the organ's transform after dropping
        Organ.transform.parent = null;
        Organ.GetComponent<Rigidbody>().isKinematic = false;
        Organ.GetComponent<MeshCollider>().enabled = true;

        // Set a default position and rotation
        Organ.transform.position = new Vector3(0f, 0f, 0f); // Replace with desired default position
        Organ.transform.rotation = Quaternion.identity; // Default rotation
    }

    void Equip()
    {
        Debug.Log("Equipping the organ");

        Organ.GetComponent<Rigidbody>().isKinematic = true;

        // Set the local position and scale relative to the parent
        Organ.transform.localPosition = Vector3.zero;
        Organ.transform.localScale = Vector3.one; // Ensure the scale is set to (1, 1, 1)
        Debug.Log("Organ Scale: " + Organ.transform.localScale);

        Organ.GetComponent<MeshCollider>().enabled = false;

        Organ.transform.parent = OrganParent;

        // Ensure the MeshRenderer is enabled
        MeshRenderer organRenderer = Organ.GetComponent<MeshRenderer>();
        if (organRenderer != null)
        {
            organRenderer.enabled = true;
        }
        else
        {
            Debug.LogError("MeshRenderer component not found on the organ!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Equip();
            }
        }
    }

    void OnBecameInvisible()
    {
        Debug.Log("Organ became invisible!");
    }

    void OnBecameVisible()
    {
        Debug.Log("Organ became visible!");
    }
}
