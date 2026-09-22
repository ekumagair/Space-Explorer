using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProperties : MonoBehaviour
{
    public byte levelNumber = 1;

    public AudioSource AudioSource { private set; get; }

    void Start()
    {
        StaticClass.currentLevel = levelNumber;
        AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (LagGlobalTimeScale.pause == true)
        {
            AudioSource.Pause();
        }
        else
        {
            AudioSource.UnPause();
        }
    }
}