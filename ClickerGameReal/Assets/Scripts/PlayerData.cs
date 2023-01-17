using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;
using System;

[Serializable]
public class PlayerData
{
    public GameManager game;

    public bool offlineProgressCheck;


    public BigDouble coins;
    public BigDouble coinsClickValue;

    //public double clickUpgrade1Cost;
    public BigDouble clickUpgrade1Level;

    //public BigDouble clickUpgrade2Cost;
    public BigDouble clickUpgrade2Level;


    //public BigDouble productionUpgrade1Cost;
    public BigDouble productionUpgrade1Level;

    //public BigDouble productionUpgrade2Cost;
    public BigDouble productionUpgrade2Power;
    public BigDouble productionUpgrade2Level;

    public BigDouble gems;
    public BigDouble gemsToGet;


    //SmoothProgressbar
    public BigDouble coinsTemp;
    //SmoothProgressbar




    //Achievements
    public BigDouble achLevel1;
    public BigDouble achLevel2;

    public BigDouble coinsCollected = 0;




    #region Prestige
    public int prestigeUlevel1;
    public int prestigeUlevel2;
    public int prestigeUlevel3;
    #endregion



    #region Automators
    public int autoLevel1;
    public int autoLevel2;
    #endregion

    public BigDouble marsCoins;

    public BigDouble coinsGains;


    #region Settings
    public short notationType;
    #endregion


}
