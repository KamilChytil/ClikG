using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;
using System;
using static GameManager;

public class MarsManager : MonoBehaviour
{
    public GameManager game;
    public PlayerData data;
    public TMP_Text marsCoinsText;
    public void Update()
    {
        var data = game.data;

        if (game.mainMenuGroupMars.gameObject.activeSelf)
        {
            marsCoinsText.text = $"MarsCoins: {Methods.NotationMethod(data.marsCoins, "F2")}";

        }

    }

    public void ClickMars()
    {
        var data = game.data;
        data.marsCoins *= 1.01;
        Debug.Log("Mars");
    }


}
