using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (_player.transform.position.x > transform.position.x && StaticClass.passedCheckpoint == false && PlayerScript.completedLevel == false && PlayerScript.isAlive)
        {
            StaticClass.passedCheckpoint = true;
            StaticClass.checkpointX = transform.position.x;
            StaticClass.checkpointY = transform.position.y;
        }
    }
}
