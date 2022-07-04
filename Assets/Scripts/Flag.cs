using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    public GameObject touchSound; // Optional.
    public Sprite touched;
    public bool playSound;
    GameObject levelP;

    SpriteRenderer _sr;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        levelP = GameObject.FindGameObjectWithTag("LevelProperties");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && PlayerScript.completedLevel == false)
        {
            StartCoroutine(Completed());
        }
    }

    IEnumerator Completed()
    {
        PlayerScript.completedLevel = true;
        StaticClass.passedCheckpoint = false;
        StaticClass.checkpointX = 0;
        StaticClass.checkpointY = 0;
        PlayerPrefs.SetInt("level", StaticClass.currentLevel);
        PlayerPrefs.SetInt("score", StaticClass.score);
        PlayerPrefs.SetInt("highScore", StaticClass.highScore);
        PlayerPrefs.SetInt("lives", StaticClass.lives);
        PlayerPrefs.SetInt("upgrade", PlayerScript.weaponUpgrade);
        PlayerPrefs.Save();

        _sr.sprite = touched;

        if (touchSound != null)
        {
            Instantiate(touchSound, transform.position, transform.rotation);
        }
        if (playSound == true)
        {
            GetComponent<AudioSource>().Play();
        }

        levelP.GetComponent<AudioSource>().mute = true;

        GameObject[] enemyShot;
        enemyShot = GameObject.FindGameObjectsWithTag("EnemyShot");
        foreach (GameObject s in enemyShot)
        {
            Destroy(s);
        }

        StaticClass.currentLevel++;

        yield return new WaitForSeconds(4f);

        if (StaticClass.currentLevel < 7)
        {
            SceneManager.LoadScene("Level" + StaticClass.currentLevel);
        }
        else
        {
            SceneManager.LoadScene("SpaceEnd");
        }
    }
}
