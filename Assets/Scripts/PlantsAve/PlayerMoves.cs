using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
    public float speed = 5f; // Adjust the speed as needed

    void Update()
    {
        // Get input from the keyboard or other input devices
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Move the player based on the input
        MovePlayer(movement);
    }

    void MovePlayer(Vector3 movement)
    {
        // Translate the player's position based on the movement direction
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
