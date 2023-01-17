using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;
using static GameManager;

public class PrestigeManager : MonoBehaviour
{
    public GameManager game;



    public Canvas presige;


    public TMP_Text[] costText = new TMP_Text[3];


    public string[] costDesc;

    public BigDouble[] costs;

    private BigDouble cost1 => 5 * BigDouble.Pow(1.5, game.data.prestigeUlevel1);
    private BigDouble cost2 => 10 * BigDouble.Pow(1.5, game.data.prestigeUlevel2);
    private BigDouble cost3 => 100 * BigDouble.Pow(2.5, game.data.prestigeUlevel3);

    public TMP_Text gemsText;
    public TMP_Text gemBoostText;
    public TMP_Text gemsToGetText;


    public int[] levels;


    public void StartPrestige()
    {
        costs = new BigDouble[3];
        levels = new int[3];
        costDesc = new string[] { "Click is 50% more effective", "You gain 10% more coins per second", "Gems are +1.01x better" };
    }



    public void Run()
    {
        var data = game.data;
        ArrayManager();
        UI();
        data.gemsToGet = 100 * Sqrt(data.coins / 1e7);

        gemsText.text = "Games: " + Methods.NotationMethod(Floor(data.gems),"F2");
        gemBoostText.text = Methods.NotationMethod(ToTalGemBoost(),"F2") + "x boost";


        if (game.mainMenuGroup.gameObject.activeSelf)
        {
            gemsToGetText.text = "Prestige:\n+" + Floor(data.gemsToGet).ToString("F0") + "Gems";
           
        }

        void UI()
        {
            if (presige.gameObject.activeSelf)
            {
                for (var i = 0; i< costText.Length; i++)
                {
                    costText[i].text = $"Level {levels[i]}\n  {costDesc[i]}\nCost: {costs[i]} Gems";
                }
            }
        }
    }

    public void BuyUpgrade(int id)
    {
        var data = game;

        switch(id)
        {
            case 0:
                Buy(ref data.data.prestigeUlevel1);
                break;
            case 1:
                Buy(ref data.data.prestigeUlevel2);
                break;
            case 2:
                Buy(ref data.data.prestigeUlevel3);
                break;
        }

        void Buy(ref int level)
        {
            if (data.data.gems < costs[id]) return;  
                data.data.gems -= costs[id];
                level++;
            

        }

    }


    public void ArrayManager()
    {
        var data = game;


        costs[0] = cost1;
        costs[1] = cost2;
        costs[2] = cost3;

        levels[0] = data.data.prestigeUlevel1;
        levels[1] = data.data.prestigeUlevel2;
        levels[2] = data.data.prestigeUlevel3;
    }
    public void Prestige()
    {

        var data = game.data;
        if (data.coins > 1000)
        {
            data.coins = 0;
            data.coinsClickValue = 1;
            //Click upgrade1
            //clickUpgrade1Cost = 10;
            data.clickUpgrade1Level = 0;
            //Click upgrade2
            //clickUpgrade2Cost = 100;
            data.clickUpgrade2Level = 0;
            //Production1
            //productionUpgrade1Cost = 25;
            data.productionUpgrade1Level = 0;
            //Production2
            //productionUpgrade2Cost = 250;
            data.productionUpgrade2Power = 5;
            data.productionUpgrade2Level = 0;
            data.gems += data.gemsToGet;


            #region Settings
            data.notationType = 0;
            #endregion

        }
    }
    public BigDouble ToTalGemBoost()
    {
        var temp = game.data.gems;
        temp *= 0.05 + (levels[2] * 0.01);
        return temp + 1;
    }








}
