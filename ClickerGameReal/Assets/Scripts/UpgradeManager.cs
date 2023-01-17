using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;
using System;
using static GameManager;

public class UpgradeManager : MonoBehaviour
{
    public GameManager game;

    //Unluck Upgrades
    public GameObject[] clickUpgrade = new GameObject[2];
    public GameObject[] productionUpgrade = new GameObject[2];

    public TMP_Text[] clickUpgradeText = new TMP_Text[2];
    public TMP_Text[] clickUpgradeMaxText = new TMP_Text[2];
    public TMP_Text[] productionUpgradeText = new TMP_Text[2];
    public TMP_Text[] productionUpgradeMaxText = new TMP_Text[2];

    public TMP_Text buyModeText;
    public int buyMode;


    public Image clickUpgrade1Bar;
    public Image clickUpgrade1BarSmmoth;

    public BigDouble[] clickUpgradeCost;
    public BigDouble[] productionUpgradeCost;
    public BigDouble[] clickUpgradePower;
    public BigDouble[] clickUpgradeLevels;
    public BigDouble[] clickUpgradeBaseCosts;
    public double[] clickUpgradeBaseCostsMults;
    public BigDouble[] clickUpgradeUnluckCosts;


    public BigDouble[] productionUpgradeLevels;

    public BigDouble[] productionUpgradeBaseCosts;
    public BigDouble[] productionUpgradeUnluckCosts;
    public double[] productionUpgradeBaseCostsMults;
    private void Start()
    {
        clickUpgradeCost = new BigDouble[2];

        clickUpgradeBaseCosts = new BigDouble[]{ 10,25 };
        clickUpgradePower = new BigDouble[] { 1, 5 };
        clickUpgradeBaseCostsMults = new [] { 1.07, 1.07 };
        clickUpgradeLevels = new BigDouble[2];
        clickUpgradeUnluckCosts = new BigDouble[] { 5, 25 };
        

        productionUpgradeCost = new BigDouble[2];
        productionUpgradeLevels = new BigDouble[2];
        productionUpgradeBaseCosts = new BigDouble[] { 25, 250 };
        productionUpgradeBaseCostsMults = new[] { 1.07, 1.07 };
        productionUpgradeUnluckCosts = new BigDouble[] { 15, 250 };


    }

    public void RunUpgrades()
    {
        var data = game.data;
        ArrayManager();
        //Zvyšování ceny!
        clickUpgradeCost[0] = clickUpgradeBaseCosts[0] * Pow(clickUpgradeBaseCostsMults[0], data.clickUpgrade1Level);
        clickUpgradeCost[1] = clickUpgradeBaseCosts[1] * Pow(clickUpgradeBaseCostsMults[1], data.clickUpgrade2Level);
        productionUpgradeCost[0] = productionUpgradeBaseCosts[0] * Pow(productionUpgradeBaseCostsMults[0], data.productionUpgrade1Level);
        productionUpgradeCost[1] = productionUpgradeBaseCosts[1] * Pow(productionUpgradeBaseCostsMults[1], data.productionUpgrade2Level);
    }


    public void RunUpgradesUI()
    {
        var data = game.data;

        string GetUpgradeCost(int index,BigDouble[] upgrade)
        {
            return Methods.NotationMethod(upgrade[index], "F2");
        }


        string GetUpgradeLevel(int index,BigDouble[] upgradelevel)
        {
            return Methods.NotationMethod(upgradelevel[index], "F2");
        }


         
        for (var i = 0; i < 2; i++)
        {

            clickUpgradeText[i].text = $"Click Upgrade {i + 1}\nCost: {GetUpgradeCost(i, clickUpgradeCost)} coins\nPower: {clickUpgradePower[i]} Click\nLevel: {GetUpgradeLevel(i, clickUpgradeLevels)}";
            clickUpgradeMaxText[i].text = $"Buy Max ({BuyClickUpgradeMaxCount(i)})";

            productionUpgradeText[i].text =  $"Production Upgrade {i}\nCost: {GetUpgradeCost(i, productionUpgradeCost)} coins\nPower: +{Methods.NotationMethod(game.ToTalBoost() * Pow(1.1, game.prestige.levels[1]),"F2")} coins/s\nLevel: {GetUpgradeLevel(i, productionUpgradeLevels)}";
            productionUpgradeMaxText[i].text = $"Buy Max ({BuyProductionUpgradeMaxCount(i)})";


            clickUpgrade[i].gameObject.SetActive(data.coinsCollected >= clickUpgradeUnluckCosts[i]);
            productionUpgrade[i].gameObject.SetActive(data.coinsCollected >= productionUpgradeUnluckCosts[i]);

        }
        Methods.BigDoubleFill(data.coins,clickUpgradeCost[0], clickUpgrade1Bar);
        Methods.BigDoubleFill(data.coinsTemp, clickUpgradeCost[1], clickUpgrade1BarSmmoth);



    }

    private void ArrayManager()
    {
        var data = game.data;
        clickUpgradeLevels[0] = data.clickUpgrade1Level;
        clickUpgradeLevels[1] = data.clickUpgrade2Level;
        productionUpgradeLevels[0] = data.productionUpgrade1Level;
        productionUpgradeLevels[1] = data.productionUpgrade2Level;
    } 
    private void NonArrayManager()
    {
        var data = game.data;
        data.clickUpgrade1Level = clickUpgradeLevels[0];
        data.clickUpgrade2Level = clickUpgradeLevels[1];

        data.productionUpgrade1Level = productionUpgradeLevels[0];
        data.productionUpgrade2Level = productionUpgradeLevels[1];
    }

    public void BuyClickUpgradeMax(int index)
    {
        var data = game.data;
        var b = clickUpgradeBaseCosts[index];
        var c = data.coins;
        var r = clickUpgradeBaseCostsMults[index];
        var k = clickUpgradeLevels[index];
        BigDouble n = 0;
        switch (buyMode)
        {
            case 0:
                n = Floor(Log(c * (r - 1) / (b * Pow(r, k)) + 1, r));
                break;
            case 1:
                n = 5;
                break;
            case 2:
                n = 10;
                break;
            case 3:
                n = 100;
                break;


        }

        var cost = b * (Pow(r, k) * (Pow(r, n) - 1) / (r - 1));

        if (data.coins >= cost)
        {
            clickUpgradeLevels[index] += n;
            data.coins -= cost; 
            data.coinsClickValue += n * clickUpgradePower[index];
        }
        NonArrayManager();

    }

    public void ChangeBuyMode()
    {


        switch (buyMode)
        {
            case 0:
                buyMode = 1;
                buyModeText.text = "Buy Mode: 5";
                break;
            case 1:
                buyMode = 2;
                buyModeText.text = "Buy Mode: 10";

                break;
            case 2:
                buyMode = 3;
                buyModeText.text = "Buy Mode: 100";

                break;
            case 3:
                buyMode = 0;
                buyModeText.text = "Buy Mode: Max";

                break;


        }

    }


    public BigDouble BuyClickUpgradeMaxCount(int index)
    {
        var data = game.data;
        var b = clickUpgradeBaseCosts[index];
        var c = data.coins;
        var r = clickUpgradeBaseCostsMults[index];
        var k = clickUpgradeLevels[index];

        BigDouble n = 0;
        switch (buyMode)
        {
            case 0:
                return Floor(Log(c * (r - 1) / (b * Pow(r, k)) + 1, r));
            case 1:
                return 5;
            case 2:
                return 10;
            case 3:
                return 100;
        }
        return n;
    }

    public void BuyUpgradeClick(int index)
    {

        var data = game.data;

        if (data.coins >= clickUpgradeCost[index])
        {

            clickUpgradeLevels[index]++;
            data.coins -= clickUpgradeCost[index];
            data.coinsClickValue += clickUpgradePower[index];
        }

        NonArrayManager();


    }
    public void BuyProductionUpgradeMax(int index)
    {
        var data = game.data;
        var b = productionUpgradeBaseCosts[index];
        var c = data.coins;
        var r = productionUpgradeBaseCostsMults[index];
        var k = productionUpgradeLevels[index];
        BigDouble n = 0;
        switch (buyMode)
        {
            case 0:
                n = Floor(Log(c * (r - 1) / (b * Pow(r, k)) + 1, r));
                break;
            case 1:
                n = 5;
                break;
            case 2:
                n = 10;
                break;
            case 3:
                n = 100;
                break;


        }

        var cost = b * (Pow(r, k) * (Pow(r, n) - 1) / (r - 1));

        if (data.coins >= cost)
        {
            productionUpgradeLevels[index] += n;
            data.coins -= cost;
        }
        NonArrayManager();

    }
    public BigDouble BuyProductionUpgradeMaxCount(int index)
    {
        var data = game.data;
        var b = productionUpgradeBaseCosts[index];
        var c = data.coins;
        var r = productionUpgradeBaseCostsMults[index];
        var k = productionUpgradeLevels[index];
        BigDouble n = 0;
        switch (buyMode)
        {
            case 0:
                return Floor(Log(c * (r - 1) / (b * Pow(r, k)) + 1, r));
            case 1:
                return 5;
            case 2:
                return 10;
            case 3:
                return 100;
        }
        return n;
    }

    public void BuyUpgradeProduction(int index)
    {

        var data = game.data;

        if (data.coins >= productionUpgradeCost[index])
        {

            productionUpgradeLevels[index]++;
            data.coins -= productionUpgradeCost[index];
        }

        NonArrayManager();


    }


}
