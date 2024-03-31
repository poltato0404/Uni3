using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerControScript : MonoBehaviour, IDataPersistence
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraMain;
    public bool isWalking = false;

    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    AudioSource footsteps;

    private PlayerController playerControls;

    private void Awake()
    {
        footsteps = GetComponent<AudioSource>();
        playerControls = new PlayerController();
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        cameraMain = Camera.main.transform;
    }

    public void SaveData(ref GameData data)
    {
        try{Vector3 playerV3 = transform.position;
        data.playerPos = playerV3;}
        catch{
            Debug.Log("Cant save");
        }
    }
    public void LoadData(GameData data)
    {
        transform.position = data.playerPos;
        data.playerPos.y = 1.5f;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movementInput = playerControls.Player_actionmap.movePlayer.ReadValue<Vector2>();
        Vector3 move = (cameraMain.forward * movementInput.y + cameraMain.right * movementInput.x);
        move.y = 0f;
        move.Normalize();
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            footsteps.Play();
            isWalking = true;
            gameObject.transform.forward = move;
        }
        else
        {
            footsteps.Stop();
            isWalking = false;
        }
        

        // Changes the height position of the player..
        if (playerControls.Player_actionmap.flash.triggered && groundedPlayer)
        {
            // playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
