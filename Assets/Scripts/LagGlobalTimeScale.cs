using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LagGlobalTimeScale : MonoBehaviour
{
    public static bool pause = false;
    AudioSource _as;

    void Start()
    {
        pause = false;
        Time.timeScale = 1.0f;
        _as = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Lag

        if (pause == false)
        {
            if (StaticClass.lagObjs <= 5)
            {
                StaticClass.lagLevel = 0;
                Time.timeScale = 1.0f;
            }
            else if (StaticClass.lagObjs > 5 && StaticClass.lagObjs <= 7)
            {
                StaticClass.lagLevel = 1;
                Time.timeScale = 0.9f;
            }
            else if (StaticClass.lagObjs > 7 && StaticClass.lagObjs <= 9)
            {
                StaticClass.lagLevel = 2;
                Time.timeScale = 0.7f;
            }
            else if (StaticClass.lagObjs > 9 && StaticClass.lagObjs <= 11)
            {
                StaticClass.lagLevel = 3;
                Time.timeScale = 0.5f;
            }
        }

        // Pause

        if (Input.GetKeyDown(KeyCode.Return))
        {
            _as.Play();

            if (pause == false)
            {
                pause = true;
                Time.timeScale = 0.0f;
            }
            else
            {
                pause = false;
                Time.timeScale = 1.0f;
            }
        }
    }
}
