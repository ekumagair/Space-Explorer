using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1.0f;
        LagGlobalTimeScale.pause = false;
        StaticClass.hardMode = true;
        PlayerPrefs.SetInt("highScore", StaticClass.highScore);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayerPrefs.Save();
            SceneManager.LoadScene("SpaceTitle");
        }
    }
}
