using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;
using System;
using static GameManager;

public class Settings : MonoBehaviour
{
    public GameManager game;
    public TMP_Text notationHyperText;
    // Notation Key
    // 0 = Sci 1 = Engineering 2 = Letter (1000 = a) 3+ = N/A


    private void Start()
    {
        UpdateNotationText();
    }
    private void UpdateNotationText()
    {
        var note = game.data.notationType;

        switch (note)
        {
            case 0:
                notationHyperText.text = "Scrientific Notation";
                break;
            case 1:
                notationHyperText.text = "Engineering Notation";
                break;
        }
    }

    public void ChangeNOtation()
    {
        var note = game.data.notationType;

        switch(note)
        {
            case 0:
                note = 1;
                break;
            case 1:
                note = 2;
                break;
            case 2:
                note = 0;
                break;
        }
        UpdateNotationText();

        game.data.notationType = note;
        Methods.NotationSettings = game.data.notationType;
    }

}
