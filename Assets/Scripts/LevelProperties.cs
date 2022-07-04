using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProperties : MonoBehaviour
{
    public byte levelNumber = 1;
    AudioSource _as;

    void Start()
    {
        StaticClass.currentLevel = levelNumber;
        _as = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(LagGlobalTimeScale.pause == true)
        {
            _as.Pause();
        }
        else
        {
            _as.UnPause();
        }
    }
}