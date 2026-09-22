using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [Header("Components")]
    public Text highScoreText;
    public Text creditText;
    public Text versionText;

    [Header("Sounds")]
    public GameObject soundDefeat;

    private int extraZero;
    private string extraZeroText;
    private float deleteTime = 0;

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

        creditText.gameObject.SetActive(true);
        versionText.gameObject.SetActive(false);
        versionText.text = "VERSION " + Application.version;

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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Level" + StaticClass.currentLevel.ToString());
        }

#if !UNITY_EDITOR && !UNITY_WEBGL && UNITY_STANDALONE
        // Quit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
#endif

        // Delete
        if (Input.GetKey(KeyCode.Delete))
        {
            deleteTime += Time.deltaTime;

            if (deleteTime > 3)
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
        if (StaticClass.debug)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                StaticClass.hardMode = true;
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                SceneManager.LoadScene("LevelTest");
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                PlayerPrefs.SetInt("highScore", 0);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                ScreenCapture.CaptureScreenshot("space explorer " + Random.Range(0, 10000));
            }
        }
    }

    private void DisplayHighScore()
    {
        extraZero = 6 - StaticClass.highScore.ToString().Length;
        extraZeroText = "";

        for (int i = 0; i < extraZero; i++)
        {
            extraZeroText = extraZeroText + "0";
        }

        highScoreText.text = "HIGH SCORE: " + extraZeroText + StaticClass.highScore.ToString();
    }

    private void MenuSetValue()
    {
        StaticClass.highScore = PlayerPrefs.GetInt("highScore");

        PlayerPrefs.Save();
    }

    public void ToggleVersion()
    {
        creditText.gameObject.SetActive(!creditText.gameObject.activeSelf);
        versionText.gameObject.SetActive(!versionText.gameObject.activeSelf);
    }
}
