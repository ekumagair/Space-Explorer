using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    public GameObject touchSound; // Optional.
    public Sprite touched;
    public bool playSound;

    private LevelProperties _levelProperties;
    private SpriteRenderer _sr;
    private AudioSource _as;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _levelProperties = GameObject.FindGameObjectWithTag("LevelProperties").GetComponent<LevelProperties>();
        _as = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerScript.completedLevel == false)
        {
            StartCoroutine(Completed());
        }
    }

    private IEnumerator Completed()
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
            _as.Play();
        }

        _levelProperties.AudioSource.mute = true;

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
