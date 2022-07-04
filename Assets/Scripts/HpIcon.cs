using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpIcon : MonoBehaviour
{
    Image img;
    public int number;

    void Start()
    {
        img = GetComponent<Image>();
    }

    void Update()
    {
        if(PlayerScript.hp < number)
        {
            img.enabled = false;
        }
        else
        {
            img.enabled = true;
        }
    }
}
