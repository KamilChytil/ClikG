using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;
using System;
using static GameManager;


public class AchievementManager : MonoBehaviour
{
    public GameManager game;
    private static string[] AchievementStrings => new string[] { "Current coins", "Total Coins Collected" };
    private BigDouble[] AchievementNumber => new BigDouble[] { game.data.coins, game.data.coinsCollected };


    public GameObject achievementScreen;
    public List<Achievements> achievementList = new List<Achievements>();


    private void Start()
    {
        foreach (var x in achievementScreen.GetComponentsInChildren<Achievements>())
            achievementList.Add(x);
    }

    public void RunAchievements()
    {

        var data = game.data;
        UpdateAchievements(AchievementStrings[0], AchievementNumber[0], ref data.achLevel1, ref achievementList[0].fill, ref achievementList[0].title, ref achievementList[0].progress);
        UpdateAchievements(AchievementStrings[1], AchievementNumber[1], ref data.achLevel2, ref achievementList[1].fill, ref achievementList[1].title, ref achievementList[1].progress);
    }

    public void UpdateAchievements(string name, BigDouble number, ref BigDouble level, ref Image fill, ref TMP_Text title, ref TMP_Text progress)
    {
        var cap = BigDouble.Pow(10, level);

        if (game.achievementGroup.gameObject.activeSelf)
        {
            title.text = $"{name}\n({level})";
            progress.text = $"{Methods.NotationMethod(number, "F2")}/{Methods.NotationMethod(cap, "F2")}";
            Methods.BigDoubleFill(number, cap, fill);

        }

        if (number < cap) return;
        BigDouble levels = 0;
        if (number / cap >= 1)
        {
            levels = Floor(Log10(number / cap)) + 1;
        }
        level += levels;
    }


}
