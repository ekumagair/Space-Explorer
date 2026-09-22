using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpIcon : MonoBehaviour
{
    public int number;

    private Image _img;

    void Start()
    {
        _img = GetComponent<Image>();
    }

    void Update()
    {
        if (PlayerScript.hp < number)
        {
            _img.enabled = false;
        }
        else
        {
            _img.enabled = true;
        }
    }
}
