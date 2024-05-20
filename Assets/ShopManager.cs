using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour, IDataPersistence
{
    public int flashLevel;
    public int shoeLevel;
    public int drinkLevel;
    public Button flashButton;
    public Button drinkButton;
    public Button shoeButton;
    public int coins;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI flashPrice;
    public TextMeshProUGUI shoePrice;
    public TextMeshProUGUI drinkPrice;

    public TextMeshProUGUI flashTitle;
    public TextMeshProUGUI shoeTitle;
    public TextMeshProUGUI drinkTitle;
    public VAFeedback va;

    public void SaveData(ref GameData data)
    {
        data.playerCoins = coins;
        data.flashLevel = flashLevel;
        data.shoeLevel = shoeLevel;
        data.drinkLevel = drinkLevel;
    }

    public void LoadData(GameData data)
    {
        coins = data.playerCoins;
        coinText.text = data.playerCoins.ToString();
        flashPrice.text = (data.flashLevel * 10).ToString();
        shoePrice.text = (data.shoeLevel * 10).ToString();
        drinkPrice.text = (data.drinkLevel * 10).ToString();
        flashLevel = data.flashLevel;
        shoeLevel = data.shoeLevel;
        drinkLevel = data.drinkLevel;
    }

    public void buyFlash()
    {
        int flashUpgradePrice = (10 * flashLevel); // Set the price for the flash item to 10 coins
        if (coins >= flashUpgradePrice)
        {
            coins -= flashUpgradePrice; // Deduct the price
            flashLevel++;
            coinText.text = coins.ToString();
            flashPrice.text = (flashLevel * 10).ToString();
        }
        else
        {
            Transform target = transform;
            va.WrongAnswer(target);
        }

    }

    public void buyShoes()
    {
        int shoeUpgradePrice = (10 * shoeLevel); // Set the price for the shoes item to 10 coins
        if (coins >= shoeUpgradePrice)
        {
            coins -= shoeUpgradePrice; // Deduct the price
            shoeLevel++;
            coinText.text = coins.ToString();
            shoePrice.text = (10 * shoeLevel).ToString();
        }
        else
        {
            Transform target = transform;
            va.WrongAnswer(target);
        }

    }

    public void buyDrinks()
    {
        int drinkUpgradePrice = (10 * drinkLevel); // Set the price for the drinks item to 10 coins
        if (coins >= drinkUpgradePrice)
        {
            coins -= drinkUpgradePrice; // Deduct the price
            drinkLevel++;
            coinText.text = coins.ToString();
            drinkPrice.text = (10 * drinkLevel).ToString();
        }
        else
        {
            Transform target = transform;
            va.WrongAnswer(target);
        }


    }
    void Update()
    {
        coinText.text = coins.ToString();
        if (coins < (flashLevel * 10) && flashLevel != 3) { flashPrice.text = "Not enough Coins"; }
        if (coins < (shoeLevel * 10) && shoeLevel != 3) { shoePrice.text = "Not enough Coins"; }
        if (coins < (drinkLevel * 10) && drinkLevel != 3) { drinkPrice.text = "Not enough Coins"; }


        switch (flashLevel)
        {
            case 1:
                flashTitle.text = "FLASHLIGHT I";
                break;
            case 2:
                flashTitle.text = "FLASHLIGHT II";
                break;
            case 3:
                flashTitle.text = "FLASHLIGHT III";
                break;
        }

        switch (drinkLevel)
        {
            case 1:
                drinkTitle.text = "ENERGY DRINK I";
                break;
            case 2:
                drinkTitle.text = "ENERGY DRINK II";
                break;
            case 3:
                drinkTitle.text = "ENERGY DRINK III";
                break;
        }

        switch (shoeLevel)
        {
            case 1:
                shoeTitle.text = "SHOE I";
                break;
            case 2:
                shoeTitle.text = "SHOE II";
                break;
            case 3:
                shoeTitle.text = "SHOE III";
                break;
        }

        if (drinkLevel == 3)
        {
            drinkButton.interactable = false;
            drinkPrice.text = "MAX";
        }
        if (shoeLevel == 3)
        {
            shoeButton.interactable = false;
            shoePrice.text = "MAX";
        }
        if (flashLevel == 3)
        {
            flashButton.interactable = false;
            flashPrice.text = "MAX";
        }

    }

}
