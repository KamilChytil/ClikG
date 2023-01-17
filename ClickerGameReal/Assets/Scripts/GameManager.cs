using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;
using System;

public class GameManager : MonoBehaviour
{
    public PrestigeManager prestige;
    public PlayerData data;
    public AutomationManager auto;
    public AchievementManager achievements;
    public UpgradeManager upgrades;
    public MarsManager mars;
    public PlanetManager planets;
    public OfflineManager offline;
    public Settings settings;


    //coins nahradit ArmyPower
    public TMP_Text coinText;
    public TMP_Text clickValueText;
    public TMP_Text coinsPerSecText;

    public Canvas mainMenuGroup;
    public Canvas upgradesGroup;
    public Canvas AutomationGroup;
    public Canvas achievementGroup;
    public Canvas planetGroup;
    public Canvas mainMenuGroupMars;
    public Canvas settingsGroup;

    public Canvas upgradesMarsGroup;


    public int tabSwitcher;

    public void Start()
    {   //nastavení pøi spuštìní

        Application.targetFrameRate = 60;

        mainMenuGroup.gameObject.SetActive(true);
        upgradesGroup.gameObject.SetActive(false);
        achievementGroup.gameObject.SetActive(false);
        prestige.presige.gameObject.SetActive(false);
        settingsGroup.gameObject.SetActive(false);
        upgradesMarsGroup.gameObject.SetActive(false);
        tabSwitcher = 0;

        prestige.StartPrestige();

        auto.StartAutomator();

        Load();
        data.offlineProgressCheck = true;
        TotalCoinsPerSecond();
        offline.LoadOfflineProduction();

        Methods.NotationSettings = data.notationType;

    }


    public void Load()
    {
        data.coins = Parse(PlayerPrefs.GetString("coins", "0"));
        //GEms
        data.gems = Parse(PlayerPrefs.GetString("gems", "0"));
        //Click Power
        data.coinsClickValue = Parse(PlayerPrefs.GetString("coinsClickValue", "1"));
        //Click upgrade1
        //clickUpgrade1Cost = double.Parse(PlayerPrefs.GetString("clickUpgrade1Cost", "10"));
        //clickUpgrade1Level = PlayerPrefs.GetInt("clickUpgrade1Level", 0);
        data.clickUpgrade1Level = Parse(PlayerPrefs.GetString("clickUpgrade1Level", "0"));
        //Click upgrade2
        //clickUpgrade2Cost = Parse(PlayerPrefs.GetString("clickUpgrade2Cost", "100"));
        //clickUpgrade2Level = PlayerPrefs.GetInt("clickUpgrade2Level", 0);
        data.clickUpgrade2Level = Parse(PlayerPrefs.GetString("clickUpgrade2Level", "0"));
        //Production1
        //productionUpgrade1Cost = Parse(PlayerPrefs.GetString("productionUpgrade1Cost", "25"));
        //productionUpgrade1Level = PlayerPrefs.GetInt("productionUpgrade1Level", 0);
        data.productionUpgrade1Level = Parse(PlayerPrefs.GetString("productionUpgrade1Level", "0"));
        //Production2
        //productionUpgrade2Cost = Parse(PlayerPrefs.GetString("productionUpgrade2Cost", "250"));
        data.productionUpgrade2Power = Parse(PlayerPrefs.GetString("productionUpgrade2Power", "5"));
        data.productionUpgrade2Level = Parse(PlayerPrefs.GetString("productionUpgrade2Level", "0"));
        //productionUpgrade2Level = PlayerPrefs.GetInt("productionUpgrade2Level", 0);


        data.achLevel1 = Parse(PlayerPrefs.GetString("achLevel1", "0"));
        data.achLevel2 = Parse(PlayerPrefs.GetString("achLevel1", "0"));
        data.coinsCollected = Parse(PlayerPrefs.GetString("coinsCollected", "0"));


        data.prestigeUlevel1 = PlayerPrefs.GetInt("prestigeUlevel1", 0);
        data.prestigeUlevel2 = PlayerPrefs.GetInt("prestigeUlevel2", 0);
        data.prestigeUlevel3 = PlayerPrefs.GetInt("prestigeUlevel3", 0);


        data.autoLevel1 = PlayerPrefs.GetInt("autoLevel1", 0);
        data.autoLevel2 = PlayerPrefs.GetInt("autoLevel2", 0);

        data.marsCoins = Parse(PlayerPrefs.GetString("marsCoins", "1"));


    }
    public void Save()
    {

        PlayerPrefs.SetString("coins", data.coins.ToString());

        PlayerPrefs.SetString("gems", data.gems.ToString());
        //Click Power
        PlayerPrefs.SetString("coinsClickValue", data.coinsClickValue.ToString());
        //Click upgrade1
        // PlayerPrefs.SetString("clickUpgrade1Cost", clickUpgrade1Cost.ToString());
        //PlayerPrefs.SetInt("clickUpgrade1Level", clickUpgrade1Level);
        PlayerPrefs.SetString("clickUpgrade1Level", data.clickUpgrade1Level.ToString());
        //Click upgrade2
        // PlayerPrefs.SetString("clickUpgrade2Cost", clickUpgrade2Cost.ToString());
        PlayerPrefs.SetString("clickUpgrade2Level", data.clickUpgrade2Level.ToString());
        //PlayerPrefs.SetInt("clickUpgrade2Level", clickUpgrade2Level);
        //Production1
        //PlayerPrefs.SetString("productionUpgrade1Cost", productionUpgrade1Cost.ToString());
        //PlayerPrefs.SetInt("productionUpgrade1Level", productionUpgrade1Level);
        PlayerPrefs.SetString("productionUpgrade1Level", data.productionUpgrade1Level.ToString());
        //Production2
        //PlayerPrefs.SetString("productionUpgrade2Cost", productionUpgrade2Cost.ToString());
        PlayerPrefs.SetString("productionUpgrade2Power", data.productionUpgrade2Power.ToString());
        //PlayerPrefs.SetInt("productionUpgrade2Level", productionUpgrade2Level);
        PlayerPrefs.SetString("productionUpgrade2Level", data.productionUpgrade2Level.ToString());


        PlayerPrefs.SetString("achLevel1", data.achLevel1.ToString());
        PlayerPrefs.SetString("achLevel2", data.achLevel2.ToString());
        PlayerPrefs.SetString("coinsCollected", data.coinsCollected.ToString());

        PlayerPrefs.SetInt("prestigeUlevel1", data.prestigeUlevel1);
        PlayerPrefs.SetInt("prestigeUlevel2", data.prestigeUlevel2);
        PlayerPrefs.SetInt("prestigeUlevel3", data.prestigeUlevel3);

        PlayerPrefs.SetInt("autoLevel1", data.autoLevel1);
        PlayerPrefs.SetInt("autoLevel2", data.autoLevel2);

        PlayerPrefs.SetString("marsCoins", data.marsCoins.ToString());

        PlayerPrefs.SetString("OfflineTime", DateTime.Now.ToBinary().ToString());


    }
    public void Update()
    {
        //Achievements
        achievements.RunAchievements();
        prestige.Run();

        auto.Run();

        if(upgradesGroup.gameObject.activeSelf) upgrades.RunUpgradesUI();
        upgrades.RunUpgrades();

        //SmoorhProgressbar
        Methods.SmoothNumber(ref data.coinsTemp, data.coins);


        coinText.text = "Coins: " + Methods.NotationMethod(data.coins, "F0");

        coinsPerSecText.text = Methods.NotationMethod(TotalCoinsPerSecond(), "F0") + " coins/s";

        if(mainMenuGroup.gameObject.activeSelf)
        {
            clickValueText.text = "Click: " + Methods.NotationMethod(TotalClickvalue(), "F0") + "Coins";
  
        }

        data.coins += TotalCoinsPerSecond() * Time.deltaTime;
        data.coinsCollected += TotalCoinsPerSecond() * Time.deltaTime;

        //SAVE
        saveTimer += Time.deltaTime;
        if(saveTimer >= 5)
        {
            Save();
            saveTimer = 0;
        }
        
        //END UPDATE
    }

    public void FullReset()
    {
        data.coins = 0;
        //GEms
        data.gems = 0;
        //Click Power
        data.coinsClickValue = 1;

        data.clickUpgrade1Level = 0;
        //Click upgrade2

        data.clickUpgrade2Level = 0;
        //Production1

        data.productionUpgrade1Level = 0;
        //Production2
        data.productionUpgrade2Power = 5;
        data.productionUpgrade2Level = 0;

        data.achLevel1 = 0;
        data.achLevel2 = 0;
        data.coinsCollected = 0;

        #region Prestige
        data.prestigeUlevel1 = 0;
        data.prestigeUlevel2 = 0;
        data.prestigeUlevel3 = 0;
        #endregion

        #region Autos
        data.autoLevel1 = 0;
        data.autoLevel2 = 0;
        #endregion

        auto.intervals[0] = 0;
        auto.intervals[1] = 0;

        data.marsCoins = 1;

        data.offlineProgressCheck = false;

        #region Settings
        data.notationType = 0;
        #endregion

    }

    public float saveTimer;


    public BigDouble ToTalBoost()
    {
        var temp = prestige.ToTalGemBoost();
        //Boost z nìjakého eventu
        return temp;
    }


    public BigDouble TotalCoinsPerSecond()
    {
        BigDouble temp = 0;
        temp += data.productionUpgrade1Level;
        temp += data.productionUpgrade2Power * data.productionUpgrade2Level;
        temp *= ToTalBoost();
        temp *= Pow(1.1, prestige.levels[1]);
        //temp *= planets.EMBoost;
        return temp;
    
    }

    private BigDouble TotalClickvalue()
    {
        BigDouble temp = data.coinsClickValue;

        temp *= Pow(1.5, prestige.levels[0]);
        return temp;
    }

    //click tlaèítko
    public void Click()
    {
        data.coins += TotalClickvalue();
        data.coinsCollected += TotalClickvalue();
    }



    public void ChangeTabs(string id)
    {
        DisableAll();
        switch (id)
        {
            case "Upgrades":
                upgradesGroup.gameObject.SetActive(true);
                break;
            case "Main":
                mainMenuGroup.gameObject.SetActive(true);
                break;
            case "Achievements":
                achievementGroup.gameObject.SetActive(true);
                break;            
            case "Prestige":
                prestige.presige.gameObject.SetActive(true);
                break;
            case "Automation":
                AutomationGroup.gameObject.SetActive(true);
                break;
            case "Planet":
                planetGroup.gameObject.SetActive(true);
                break;
            case "Mars":
                mainMenuGroupMars.gameObject.SetActive(true);
                break;
            case "Settings":
                settingsGroup.gameObject.SetActive(true);
                break;
            case "MarsUpgrades":
                upgradesMarsGroup.gameObject.SetActive(true);
                break;
        }

        void DisableAll()
        {
            mainMenuGroup.gameObject.SetActive(false);
            upgradesGroup.gameObject.SetActive(false);
            achievementGroup.gameObject.SetActive(false);
            prestige.presige.gameObject.SetActive(false);
            AutomationGroup.gameObject.SetActive(false);
            planetGroup.gameObject.SetActive(false);
            mainMenuGroupMars.gameObject.SetActive(false);
            settingsGroup.gameObject.SetActive(false);
            upgradesMarsGroup.gameObject.SetActive(false);

        }


    }


    public class Methods : MonoBehaviour
    {
        public static int NotationSettings;
        public static void CanvasGroupChanged(bool x, CanvasGroup y)
        {
            if (x)
            {
                y.alpha = 1;
                y.interactable = true;
                y.blocksRaycasts = true;
                return;
            }

            y.alpha = 0;
            y.interactable = false;
            y.blocksRaycasts = false;
        }

        public static void BigDoubleFill(BigDouble x, BigDouble y, Image fill)
        {
            float z;
            var a = x / y;
            if (a < 0.001)
            {
                z = 0;
            }
            else if (a > 10)
            {
                z = 1;
            }
            else
                z = (float)a.ToDouble();
            fill.fillAmount = z;
        }


        public static void SmoothNumber(ref BigDouble smooth, BigDouble actual)
        {
            if (smooth > actual && actual == 0)
            {
                smooth -= (smooth - actual) / 10 + 0.1 * Time.deltaTime;
            }
            else if (Floor(smooth) < actual)
            {
                smooth += (actual - smooth) / 10 + 0.1 * Time.deltaTime;
            }
            else if (Floor(smooth) > actual)
            {
                smooth -= (smooth - actual) / 10 + 0.1 * Time.deltaTime;
            }
            else
            {
                smooth = actual;
            }

        }


        public static string NotationMethod(BigDouble x, string y)
        {
            if (x <= 1000) return x.ToString(y);
            switch (NotationSettings)
            {
                case 0:
                     {
                     var exponent = (Floor(Log10(Abs(x))));
                     var mantissa = (x / Pow(10, exponent));
                     return mantissa.ToString(y) + "e" + exponent;

                     }
                       
                case 1:
                     {
                     var exponent = 3 * Floor(Floor(Log10(x)) / 3);
                     var mantissa = (x / Pow(10, exponent));
                     return mantissa.ToString(y) + "e" + exponent;
                     }

            }
            return "";

        }

        //public static string NotationMethod(BigDouble x, string y)
        //{
        //    if (x >= 1000)
        //    {
        //        var exponent = (Floor(Log10(Abs(x))));
        //        var mantissa = (x / Pow(10, exponent));

        //        return mantissa.ToString("F2") + "e" + exponent;
        //    }
        //    return x.ToString(y);

        //}

    }

}
