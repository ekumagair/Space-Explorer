using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProperties : MonoBehaviour
{
    public byte levelNumber = 1;

    private AudioSource _as;

    void Start()
    {
        StaticClass.currentLevel = levelNumber;
        _as = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (LagGlobalTimeScale.pause == true)
        {
            _as.Pause();
        }
        else
        {
            _as.UnPause();
        }
    }
}