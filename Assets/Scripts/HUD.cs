using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text score;
    public Text levelText;
    public Text livesText;

    private int _extraZero;
    private string _extraZeroText;

    void Start()
    {
        levelText.text = "LEVEL " + StaticClass.currentLevel;
    }

    void Update()
    {
        _extraZero = 6 - StaticClass.score.ToString().Length;
        _extraZeroText = "";

        for (int i = 0; i < _extraZero; i++)
        {
            _extraZeroText += "0";
        }

        score.text = "SCORE: " + _extraZeroText + StaticClass.score.ToString();
        livesText.text = "LIVES: " + StaticClass.lives;

        // Debug Screenshot
        if (StaticClass.debug)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                ScreenCapture.CaptureScreenshot("space explorer " + Random.Range(0, 10000) + ".png");
            }
        }
    }
}