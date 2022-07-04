using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int directionX = 1;
    public int directionY = 1;

    void Update()
    {
        transform.Translate(directionX * 2 * Time.deltaTime, directionY * 2 * Time.deltaTime, 0);
    }
}
