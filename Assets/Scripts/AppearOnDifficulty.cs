using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearOnDifficulty : MonoBehaviour
{
    public bool normalMode = true;
    public bool hardMode = true;

    void Start()
    {
        if(normalMode == false && StaticClass.hardMode == false)
        {
            Destroy(gameObject);
        }
        if (hardMode == false && StaticClass.hardMode == true)
        {
            Destroy(gameObject);
        }
    }
}
