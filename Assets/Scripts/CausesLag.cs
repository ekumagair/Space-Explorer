using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CausesLag : MonoBehaviour
{
    SpriteRenderer _sr;
    Color myColor;
    float a;

    bool added = false;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        added = false;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "MainCamera" && added == false)
        {
            StaticClass.lagObjs++;

            if (StaticClass.debug == true)
            {
                Debug.Log(StaticClass.lagObjs);
            }

            added = true;
        }
        else if(collision.gameObject.tag == "BarrierLeft" && tag != "Player")
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (StaticClass.lagObjs > 0)
        {
            StaticClass.lagObjs--;
        }
    }

    void Update()
    {
        if (StaticClass.lagLevel == 1)
        {
            if (Random.Range(0, 4) == 0)
            {
                a = Random.Range(0.6f, 1f);
            }
            else
            {
                a = 0f;
            }

            myColor = new Color(_sr.color.r, _sr.color.g, _sr.color.b, a);
        }
        else if (StaticClass.lagLevel == 2)
        {
            if (Random.Range(0, 5) == 0)
            {
                a = Random.Range(0.25f, 1f);
            }
            else
            {
                a = 0f;
            }

            myColor = new Color(_sr.color.r, _sr.color.g, _sr.color.b, a);
        }
        else if (StaticClass.lagLevel == 3)
        {
            if (Random.Range(0, 6) == 0)
            {
                a = Random.Range(0f, 1f);
            }
            else
            {
                a = 0f;
            }

            myColor = new Color(_sr.color.r, _sr.color.g, _sr.color.b, a);
        }
        else
        {
            myColor = new Color(_sr.color.r, _sr.color.g, _sr.color.b, 1);
        }

        _sr.color = myColor;
    }
}
