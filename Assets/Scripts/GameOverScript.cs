using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1.0f;
        LagGlobalTimeScale.pause = false;
        PlayerPrefs.SetInt("highScore", StaticClass.highScore);
        PlayerPrefs.Save();
        StartCoroutine(GoToMenu());
    }

    IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("SpaceTitle");
    }
}
