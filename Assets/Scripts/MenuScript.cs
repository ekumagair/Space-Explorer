using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Text highScoreText;
    public GameObject soundDefeat;

    int extraZero;
    string extraZeroText;

    float deleteTime = 0;

    private void Start()
    {
        Time.timeScale = 1.0f;
        StaticClass.currentLevel = 1;
        StaticClass.score = 0;
        StaticClass.lives = 3;
        StaticClass.passedCheckpoint = false;
        PlayerScript.weaponUpgrade = 0;
        StaticClass.lagObjs = 0;
        StaticClass.lagLevel = 0;
        LagGlobalTimeScale.pause = false;
        deleteTime = 0;

        if (PlayerPrefs.HasKey("highScore"))
        {
            MenuSetValue();
        }

        DisplayHighScore();
    }

    void Update()
    {
        // Start

        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Level" + StaticClass.currentLevel);
        }

        // Quit

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Delete

        if(Input.GetKey(KeyCode.Delete))
        {
            deleteTime += Time.deltaTime;

            if(deleteTime > 3)
            {
                deleteTime = 0;
                StaticClass.highScore = 0;
                StaticClass.hardMode = false;
                Instantiate(soundDefeat);

                PlayerPrefs.SetInt("highScore", StaticClass.highScore);

                DisplayHighScore();
                MenuSetValue();
            }
        }
        
        // DEBUG

        if (Input.GetKeyDown(KeyCode.H) && StaticClass.debug == true)
        {
            StaticClass.hardMode = true;
        }
        if (Input.GetKeyDown(KeyCode.T) && StaticClass.debug == true)
        {
            SceneManager.LoadScene("LevelTest");
        }
        if (Input.GetKeyDown(KeyCode.Delete) && StaticClass.debug == true)
        {
            PlayerPrefs.SetInt("highScore", 0);
        }
        if (Input.GetKeyDown(KeyCode.P) && StaticClass.debug == true)
        {
            ScreenCapture.CaptureScreenshot("space explorer " + Random.Range(0, 10000));
        }
    }

    void DisplayHighScore()
    {
        extraZero = 6 - StaticClass.highScore.ToString().Length;
        extraZeroText = "";

        for (int i = 0; i < extraZero; i++)
        {
            extraZeroText = extraZeroText + "0";
        }

        highScoreText.text = "HIGH SCORE: " + extraZeroText + StaticClass.highScore.ToString();
    }

    void MenuSetValue()
    {
        //StaticClass.score = PlayerPrefs.GetInt("score");
        StaticClass.highScore = PlayerPrefs.GetInt("highScore");
        //StaticClass.currentLevel = PlayerPrefs.GetInt("level");

        PlayerPrefs.Save();
    }
}
