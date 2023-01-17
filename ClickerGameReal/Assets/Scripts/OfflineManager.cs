using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;
using System;
using static GameManager;

public class OfflineManager : MonoBehaviour
{
    public GameManager game;
    public PlayerData data;

    public GameObject offlinePoUp;

    public TMP_Text timeAwayText;
    public TMP_Text earthGainsText;





    public void LoadOfflineProduction()
    {
        if(game.data.offlineProgressCheck)
        {
            //offline time management
            var tempOfflineTime = Convert.ToInt64(PlayerPrefs.GetString("OfflineTime"));
            var oldTime = DateTime.FromBinary(tempOfflineTime);

            var currentTime = DateTime.Now;

            var difference = currentTime.Subtract(oldTime);
            var rawTime = (float)difference.TotalSeconds;
            var offlineTime = rawTime;
            rawTime = MathF.Round(rawTime);
            offlinePoUp.gameObject.SetActive(true);
            TimeSpan timer = TimeSpan.FromSeconds(rawTime);
            timeAwayText.text = $"You were away for: <color=#00FFFF>{timer}</color>";

            data.coinsGains = game.TotalCoinsPerSecond() * offlineTime;

            
            earthGainsText.text = $"You earned:  <color=yellow>{Methods.NotationMethod(data.coinsGains, "F2")} Coins</color>";
        }

    }

    public void CloseOffline()
    {
        game.data.coins += data.coinsGains;
        offlinePoUp.gameObject.SetActive(false);
    }

}
