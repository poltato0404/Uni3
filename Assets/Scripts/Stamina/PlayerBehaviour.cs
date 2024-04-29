using System.Collections;
using UnityEngine;
using TMPro; 

public class PlayerBehaviour : MonoBehaviour,IDataPersistence
{
    [SerializeField] StaminaBar _staminaBar;
    [SerializeField] playerControScript _playerContro;
    [SerializeField] GameObject sprintButton; // Reference to the sprint button GameObject

    float _playerOriginalSpeed;
    float _playerSprintSpeed;
    bool isSprinting = false;
    public Vector3 playerPos;
    bool isRegenerationDelayed = false; // Flag to indicate if regeneration is delayed
    float regenerationDelayDuration = 2f; // Duration of delay in seconds
    public int numberOfDrinks;
    [SerializeField] private TMP_Text drinkTMP;
    public GameObject gameOver;
    public GameObject pausePanelBlock;
    [SerializeField] InventoryManager inventory;

    void Start()
    {
         pausePanelBlock.SetActive(false);
        gameOver.SetActive(false);
        _playerOriginalSpeed = _playerContro.playerSpeed;
        _playerSprintSpeed = _playerContro.playerSpeed * 2f;
    }

    void Update()
    {
        if (StaminaGameManager.staminaGameManager._playertStamina.Stamina  < 1)
        {
            GameOver();
        }

        playerPos = transform.position;
        // If the player is sprinting, use stamina and set sprinting speed
        if (isSprinting)
        {
            if (StaminaGameManager.staminaGameManager._playertStamina.Stamina > 0)
            {
                PlayerUseStamina(35f);
                _playerContro.playerSpeed = _playerSprintSpeed;
            }
            else
            {
                StopSprinting(); // Stop sprinting if stamina runs out
                _playerContro.playerSpeed = _playerOriginalSpeed; // Reset speed to original
                isRegenerationDelayed = true; // Set flag to delay regeneration
                StartCoroutine(DelayedRegeneration()); // Start coroutine for delayed regeneration
            }
        }
        else
        {
            if (!isRegenerationDelayed)
            {
                PlayerRegenStamina(); // Regular stamina regeneration
            }
        }
    }

    IEnumerator DelayedRegeneration()
    {
        yield return new WaitForSeconds(regenerationDelayDuration);
        isRegenerationDelayed = false; // Reset the flag
        PlayerRegenStamina(); // Resume stamina regeneration
    }

    // Method called when the player presses the sprint button
    public void StartSprinting()
    {
        isSprinting = true;
    }

    // Method called when the player releases the sprint button or when stamina runs out
    public void StopSprinting()
    {
        isSprinting = false;
    }

    private void PlayerUseStamina(float staminaAmount)
    {
        StaminaGameManager.staminaGameManager._playertStamina.UseStamina(staminaAmount);
        _staminaBar.SetStamina(StaminaGameManager.staminaGameManager._playertStamina.Stamina);
    }

    private void PlayerRegenStamina()
    {
        StaminaGameManager.staminaGameManager._playertStamina.RegenStamina();
        _staminaBar.SetStamina(StaminaGameManager.staminaGameManager._playertStamina.Stamina);
    }
     public void drinkEnergy()
    {
        if(numberOfDrinks>0){
         StaminaGameManager.staminaGameManager._playertStamina.drink();
         numberOfDrinks--;
         setDrinkText(numberOfDrinks);
         }
    }
    public void SaveData(ref GameData data) {
        
        data.numberOfDrinks = numberOfDrinks;

    }
    public void LoadData(GameData data) {
        switch(data.currentLevel){
        case 1:
            if(data.loadedLevel1){ numberOfDrinks = data.numberOfDrinks;}else{numberOfDrinks =3;}break;
        case 2:
        if(data.loadedLevel2){ numberOfDrinks = data.numberOfDrinks;}else{numberOfDrinks =6; }break;
        case 3:
        if(data.loadedLevel3){ numberOfDrinks = data.numberOfDrinks;}else{numberOfDrinks =9; }break;
        }

        setDrinkText(numberOfDrinks);
        
        
    }

    public void setDrinkText(int x){
        drinkTMP.text = x.ToString();
    }

    public void GameOver()
    {

            pausePanelBlock.SetActive(true);
            gameOver.SetActive(true);
            Time.timeScale =  0f;

    }

    void OnTriggerEnter(Collider other)
    {
        // Access the GameObject that this object collided with
        GameObject collidedObject = other.gameObject;
       
        // You can add conditions to identify the specific object you want to interact with
        if (collidedObject.CompareTag("Coin"))
        {
             Debug.Log("collide");
            InventoryItem item = collidedObject.GetComponent<InventoryItem>();
            inventory.AddItemToInventory(item);
            Destroy(collidedObject);
        }
        if (collidedObject.CompareTag("device"))
        {
             Debug.Log("collide");
            InventoryItem item = collidedObject.GetComponent<InventoryItem>();
            inventory.AddItemToInventory(item);
            Destroy(collidedObject);
        }
    }



}