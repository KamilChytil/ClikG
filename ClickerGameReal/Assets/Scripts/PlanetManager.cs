using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;
using System;
using static GameManager;

public class PlanetManager : MonoBehaviour
{
    public GameManager game;
    public Canvas Earth;
    public Canvas Mars;

    public TMP_Text earthCoinsText;
    public TMP_Text MarsCoinsText;
    public TMP_Text EMboostText;


    public BigDouble EMBoost => Log(Sqrt(game.data.coins)+1,20) + 1;


    public void Update()
    {
        var data = game.data;
        earthCoinsText.text = $"{Methods.NotationMethod(data.coins, "F2")} Coins";
        MarsCoinsText.text = $"{Methods.NotationMethod(data.marsCoins, "F2")} MarsCoins";
        EMboostText.text = $"{Methods.NotationMethod(EMBoost, "F2")}x\n Coins/s";
    }

    public void ChangeTabs(string id)
    {
        DisableAll();
        switch (id)
        {
           case "Earth":
               Earth.gameObject.SetActive(true);      
               game.mainMenuGroup.gameObject.SetActive(true);      
               break;                                 
           case "Mars":
                Mars.gameObject.SetActive(true);
                game.mainMenuGroupMars.gameObject.SetActive(true);

                break;
        }

        void DisableAll()
        {
            Earth.gameObject.SetActive(false);
            Mars.gameObject.SetActive(false);
            game.planetGroup.gameObject.SetActive(false);
            game.mainMenuGroupMars.gameObject.SetActive(false);
        }


    }      
}          
