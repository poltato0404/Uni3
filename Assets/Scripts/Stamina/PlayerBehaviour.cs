using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;

public class PlayerBehaviour : MonoBehaviour, IDataPersistence
{
    [SerializeField] StaminaBar _staminaBar;
    [SerializeField] playerControScript _playerContro;
    [SerializeField] GameObject sprintButton; // Reference to the sprint button GameObject

    float _playerOriginalSpeed;
    public InventoryItem laptop;
    float _playerSprintSpeed;
    bool isSprinting = false;

    public Vector3 playerPos;
    bool isRegenerationDelayed = false; // Flag to indicate if regeneration is delayed
    float regenerationDelayDuration = 2f; // Duration of delay in seconds
    public int numberOfDrinks;
    [SerializeField] private TMP_Text drinkTMP;
    public GameObject gameOver;
    public GameObject pausePanelBlock;
    public int numberOfCoins;
    public TextMeshProUGUI cointText;
    [SerializeField] InventoryManager inventory;
    public int stringEvidenceCount;
    public promptManager prompt;
    public int coinsCollected; //per level
    public int documentCollected; // per level

    public GameObject docuObject;
    InventoryItem item;
    [SerializeField] mapLoader mapL;

    void Start()
    {
        pausePanelBlock.SetActive(false);
        gameOver.SetActive(false);
        _playerOriginalSpeed = _playerContro.playerSpeed;
        _playerSprintSpeed = _playerContro.playerSpeed * 2f;
        item = docuObject.GetComponent<InventoryItem>();
    }

    void Update()
    {
        cointText.text = numberOfCoins.ToString();
        if (StaminaGameManager.staminaGameManager._playertStamina.Stamina < 1)
        {
            GameOver();
        }

        playerPos = transform.position;
        // If the player is sprinting, use stamina and set sprinting speed
        if (isSprinting)
        {
            if (StaminaGameManager.staminaGameManager._playertStamina.Stamina > 0)
            {
                PlayerUseStamina(13f);
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
        if (numberOfDrinks > 0)
        {
            StaminaGameManager.staminaGameManager._playertStamina.drink();
            numberOfDrinks--;
            setDrinkText(numberOfDrinks);
        }
    }
    public void SaveData(ref GameData data)
    {
        data.coinsCollected = coinsCollected;
        data.docuCollected = documentCollected;
        data.numberOfDrinks = numberOfDrinks;
        data.playerCoins = numberOfCoins;
        data.stringCountEvidence = stringEvidenceCount;

    }
    public void LoadData(GameData data)
    {
        coinsCollected = data.coinsCollected;
        documentCollected = data.docuCollected;
        if (data.isLaptopRetrieved) { inventory.AddItemToInventory(laptop); }

        numberOfCoins = data.playerCoins;
        stringEvidenceCount = data.stringCountEvidence;

        switch (data.currentLevel)
        {
            case 1:
                if (data.loadedLevel1) { numberOfDrinks = data.numberOfDrinks; }
                else
                {
                    numberOfDrinks = drinkSwitch(data.drinkLevel);
                }
                break;
            case 2:
                if (data.loadedLevel2) { numberOfDrinks = data.numberOfDrinks; }
                else
                {
                    numberOfDrinks = drinkSwitch(data.drinkLevel);
                }
                break;
            case 3:
                if (data.loadedLevel3) { numberOfDrinks = data.numberOfDrinks; }
                else
                {
                    numberOfDrinks = drinkSwitch(data.drinkLevel);
                }
                break;
        }

        switch (data.shoeLevel)
        {
            case 1:
                _playerSprintSpeed = _playerContro.playerSpeed * 1.5f;
                break;
            case 2:
                _playerSprintSpeed = _playerContro.playerSpeed * 2f;
                break;
            case 3:
                _playerSprintSpeed = _playerContro.playerSpeed * 2.5f;
                break;
        }




        setDrinkText(numberOfDrinks);
        for (int i = 0; i < data.docuCollected; i++)
        {
            Debug.Log("docutoIN");
            item.itemName = prompt.evidenceStringList[i];
            inventory.AddItemToInventory(item);
        }
    }


    public int drinkSwitch(int y)
    {
        y *= 3;
        return y;
    }

    public void setDrinkText(int x)
    {
        drinkTMP.text = x.ToString();
    }

    public void GameOver()
    {

        pausePanelBlock.SetActive(true);
        gameOver.SetActive(true);
        Time.timeScale = 0f;

    }

    void OnTriggerEnter(Collider other)
    {
        // Access the GameObject that this object collided with
        GameObject collidedObject = other.gameObject;

        // You can add conditions to identify the specific object you want to interact with
        if (collidedObject.CompareTag("Coin"))
        {
            Debug.Log("collide");
            coinsCollected++;
            numberOfCoins++;
            prompt.promptCoin();
            RemoveCoinPosition(collidedObject.transform.position);
            collidedObject.SetActive(false);

        }
        if (collidedObject.CompareTag("device"))
        {
            Debug.Log("collide");
            prompt.promptLaptop();
            InventoryItem item = collidedObject.GetComponent<InventoryItem>();
            inventory.AddItemToInventory(item);
            collidedObject.SetActive(false);
            mapL.gotLaptop = true;
        }
        if (collidedObject.CompareTag("folder"))

        {
            Debug.Log("collide");
            prompt.promptDocument(stringEvidenceCount);
            InventoryItem item = collidedObject.GetComponent<InventoryItem>();
            item.itemName = prompt.evidenceStringList[stringEvidenceCount];
            inventory.AddItemToInventory(item);
            stringEvidenceCount++;
            documentCollected++;
            removeDocu(collidedObject.transform.position);
            collidedObject.SetActive(false);
        }
    }

    private void RemoveCoinPosition(Vector3 positionToRemove)
    {
        // Search for the position in the list and remove it
        for (int i = 0; i < mapL.coinList.Count; i++)
        {
            if (mapL.coinList[i] == positionToRemove)
            {
                // Remove the position from the list
                mapL.coinList.RemoveAt(i);

                // Break the loop as we found and removed the position
                break;
            }
        }
        mapL.coinList.RemoveAll(item => item == Vector3.zero);
    }
    private void removeDocu(Vector3 positionToRemove)
    {
        // Search for the position in the list and remove it
        for (int i = 0; i < mapL.docuList.Count; i++)
        {
            if (mapL.docuList[i] == positionToRemove)
            {
                // Remove the position from the list
                mapL.docuList.RemoveAt(i);

                // Break the loop as we found and removed the position
                break;
            }
        }
        mapL.docuList.RemoveAll(item => item == Vector3.zero);
    }




}