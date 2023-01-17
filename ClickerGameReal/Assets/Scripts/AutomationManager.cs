using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;
using static GameManager;



public class AutomationManager : MonoBehaviour
{
    public GameManager game;
    public PlayerData data;
    public UpgradeManager upgrades;
    public TMP_Text[] costText = new TMP_Text[2];
    public Image[] costBars = new Image[2];


    public string[] costDesc;

    public BigDouble[] costs;


    private BigDouble cost1 => 1e4 * BigDouble.Pow(1.5, game.data.autoLevel1);
    private BigDouble cost2 => 1e5 * BigDouble.Pow(1.5, game.data.autoLevel2);

    public int[] levels;
    public int[] levelsCap;
    public float[] intervals;
    public float[] timer;



    public void StartAutomator()
    {
        costs = new BigDouble[2];
        levels = new int[2];
        levelsCap = new[] { 22, 22 };
        intervals = new float[2];
        timer = new float[2];

        costDesc = new string[] { "Click Upgrade 1 Autobuyer", "Production Upgrade 1 Autobuyer" };
    }


    public void Run()
    {
        var data = game.data;
        ArrayManager();
        UI();
        RunAuto();


        void UI()
        {
            if (game.AutomationGroup.gameObject.activeSelf)
            {
                for (var i = 0; i < costText.Length; i++)
                {
                    costText[i].text = $"{costDesc[i]}\nCost: {Methods.NotationMethod(costs[i], "F2")} Coins\nInterval:{(levels[i] >= levelsCap[i] ? "Instant" : intervals[i].ToString("F1"))}";
                    Methods.BigDoubleFill(game.data.coins, costs[i], costBars[i]);
                }
            }
        }


        void RunAuto()
        {


            AutoC(0,0);
            AutoP(1, 0);
            //Pokud se bude dìlat ID tak musí být:  void Auto(int id, string name)
            void AutoC(int id,int index)
            {
                if (levels[id] <= 0) return;
                if (levels[id] != levelsCap[id])
                {
                    timer[id] += Time.deltaTime;
                    if (!(timer[id] >= intervals[id])) return;
                    upgrades.BuyUpgradeClick(index);
                    timer[id] = 0;
                }
                else
                {
                    if (upgrades.BuyClickUpgradeMaxCount(index) != 0)

                        upgrades.BuyUpgradeClick(index);
                }

            }

            void AutoP(int id, int index)
            {
                if (levels[id] > 0)
                {
                    if (levels[id] != levelsCap[id])
                    {
                        timer[id] += Time.deltaTime;
                        if (timer[id] >= intervals[id])
                        {
                            upgrades.BuyUpgradeProduction(index);
                            timer[id] = 0;
                        }
                    }
                    else
                    {
                        if (upgrades.BuyProductionUpgradeMaxCount(index) != 0)

                            upgrades.BuyUpgradeProduction(index);
                    }

                }

            }



        }

    }





    public void BuyUpgrade(int id)
    {
        var data = game.data;

        switch (id)
        {
            case 0:
                Buy(ref data.autoLevel1);
                break;
            case 1:
                Buy(ref data.autoLevel2);
                break;
        }

        void Buy(ref int level)
        {
            if (!(data.coins >= costs[id] && level < levelsCap[id])) return;
            {
                data.coins -= costs[id];
                level++;
            }



        }

    }
    public void ArrayManager()
    {
        var data = game.data;


        costs[0] = cost1;
        costs[1] = cost2;

        levels[0] = data.autoLevel1;
        levels[1] = data.autoLevel2;

        if (data.autoLevel1 > 0)
            intervals[0] = 10.5f - ((data.autoLevel1) * 0.5f);
        else
            intervals[0] = 0;
        if (data.autoLevel2 > 0)
            intervals[1] = 10.5f - ((data.autoLevel2) * 0.5f);
        else
            intervals[1] = 0;

    }

}

