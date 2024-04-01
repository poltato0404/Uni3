using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class playerControScript : MonoBehaviour, IDataPersistence
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraMain;
    public bool isWalking = false;

    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    AudioSource footsteps;

    public Button flashButton;
    bool flashStatus;
    [SerializeField] private Sprite flashOffSprite;
    [SerializeField] private Sprite flashOnSprite;
    public GameObject flashLight;
    public float flashLevel = 2.5f;
    private PlayerController playerControls;

    private void Awake()
    {
        flashStatus = false;
        playerSpeed = 3f;
        flashLight.GetComponent<Light>().intensity = 0;
        footsteps = GetComponent<AudioSource>();
        playerControls = new PlayerController();
        controller = GetComponent<CharacterController>();
    }
    public void toggleFlash()
    {
        if (flashStatus)
        {
            flashOff();
        }
        else
        {
            flashOn();
        }
    }

    private void flashOn()
    {
        flashButton.image.sprite = flashOnSprite;
        flashLight.GetComponent<Light>().intensity = flashLevel;
        flashStatus = true;
    }

    private void flashOff()
    {
        flashButton.image.sprite = flashOffSprite;
        flashLight.GetComponent<Light>().intensity = 0;
        flashStatus = false;
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    public void isSprinting()
    {
        playerSpeed = 5f;
    }

    public void notSprinting()
    {
        playerSpeed = 3f;
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
