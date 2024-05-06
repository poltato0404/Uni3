using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class ShopManager : MonoBehaviour, IDataPersistence
{
    public int flashLevel;
    public int shoeLevel;
    public int drinkLevel;
    public int coins;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI flashPrice;
    public TextMeshProUGUI shoePrice;
    public TextMeshProUGUI drinkPrice;
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
        flashPrice.text = data.flashLevel.ToString();
        shoePrice.text = data.shoeLevel.ToString();
        drinkPrice.text = data.drinkLevel.ToString();
        flashLevel = data.flashLevel;
        shoeLevel = data.shoeLevel;
        drinkLevel = data.drinkLevel;
    }

    public void buyFlash()
    {
        if (coins > flashLevel)
        {
            coins -= flashLevel;
            flashLevel++;
            coinText.text = coins.ToString();
            flashPrice.text = flashLevel.ToString();
        }
        else
        {
            Transform target = transform;

            va.WrongAnswer(target);
        }

    }

    public void buyshoes()
    {

        if (coins > shoeLevel)
        {
            coins -= shoeLevel;
            shoeLevel++;
            coinText.text = coins.ToString();
            shoePrice.text = shoeLevel.ToString();

        }
        else
        {
            Transform target = transform;

            va.WrongAnswer(target);
        }
    }

    public void buydrinks()
    {
        if (coins > drinkLevel)
        {
            coins -= drinkLevel;
            drinkLevel++;
            coinText.text = coins.ToString();
            drinkPrice.text = drinkLevel.ToString();
        }
        else
        {
            Transform target = transform;

            va.WrongAnswer(target);
        }
    }

}
