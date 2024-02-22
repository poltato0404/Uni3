using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipPlants : MonoBehaviour
{
    public GameObject PlantOrgan;
    public Transform Player;

    private bool isEquipped = false;

    void Start()
    {
        PlantOrgan.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isEquipped)
        {
            Debug.Log("E key pressed. Organ position: " + PlantOrgan.transform.position);
            Equip();
        }
    }

    void Drop()
    {
        // Detach the plant organ from its parent
        PlantOrgan.transform.parent = null;

        // Enable physics for the plant organ
        PlantOrgan.GetComponent<Rigidbody>().isKinematic = false;

        // Enable collider for interaction
        PlantOrgan.GetComponent<Collider>().enabled = true;
    }

    void Equip()
    {
        // Disable physics for the plant organ
        PlantOrgan.GetComponent<Rigidbody>().isKinematic = true;

        // Position the plant organ at the player's location
        PlantOrgan.transform.position = Player.position;

        // Rotation and other adjustments as needed

        // Disable collider for interaction
        PlantOrgan.GetComponent<Collider>().enabled = false;

        // Parent the plant organ to the player or a designated location
        PlantOrgan.transform.SetParent(Player);

        // Set isEquipped to true
        isEquipped = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) && !isEquipped)
            {
                Equip();
            }
        }
    }
}
