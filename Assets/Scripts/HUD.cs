using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text score;
    public Text levelText;
    public Text livesText;

    int extraZero;
    string extraZeroText;

    void Start()
    {
        levelText.text = "LEVEL " + StaticClass.currentLevel;
    }

    void Update()
    {
        extraZero = 6 - StaticClass.score.ToString().Length;
        extraZeroText = "";

        for (int i = 0; i < extraZero; i++)
        {
            extraZeroText = extraZeroText + "0";
        }

        score.text = "SCORE: " + extraZeroText + StaticClass.score.ToString();
        livesText.text = "LIVES: " + StaticClass.lives;

        // Screenshot

        if (Input.GetKeyDown(KeyCode.P) && StaticClass.debug == true)
        {
            ScreenCapture.CaptureScreenshot("space explorer " + Random.Range(0, 10000) + ".png");
        }
    }
}