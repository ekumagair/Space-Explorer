using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(player.transform.position.x > transform.position.x && StaticClass.passedCheckpoint == false && PlayerScript.completedLevel == false && PlayerScript.isAlive)
        {
            StaticClass.passedCheckpoint = true;
            StaticClass.checkpointX = transform.position.x;
            StaticClass.checkpointY = transform.position.y;
        }
    }
}
