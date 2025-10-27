using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpIcon : MonoBehaviour
{
    public int number;

    private Image img;

    void Start()
    {
        img = GetComponent<Image>();
    }

    void Update()
    {
        if (PlayerScript.hp < number)
        {
            img.enabled = false;
        }
        else
        {
            img.enabled = true;
        }
    }
}
