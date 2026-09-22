using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CausesLag : MonoBehaviour
{
    private SpriteRenderer _sr;
    private Color _myColor;
    private float _alpha;
    private bool _added = false;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _added = false;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainCamera" && _added == false)
        {
            StaticClass.lagObjs++;

            if (StaticClass.debug == true)
            {
                Debug.Log(StaticClass.lagObjs);
            }

            _added = true;
        }
        else if (collision.gameObject.tag == "BarrierLeft" && tag != "Player")
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
                _alpha = Random.Range(0.6f, 1f);
            }
            else
            {
                _alpha = 0f;
            }

            _myColor = new Color(_sr.color.r, _sr.color.g, _sr.color.b, _alpha);
        }
        else if (StaticClass.lagLevel == 2)
        {
            if (Random.Range(0, 5) == 0)
            {
                _alpha = Random.Range(0.25f, 1f);
            }
            else
            {
                _alpha = 0f;
            }

            _myColor = new Color(_sr.color.r, _sr.color.g, _sr.color.b, _alpha);
        }
        else if (StaticClass.lagLevel == 3)
        {
            if (Random.Range(0, 6) == 0)
            {
                _alpha = Random.Range(0f, 1f);
            }
            else
            {
                _alpha = 0f;
            }

            _myColor = new Color(_sr.color.r, _sr.color.g, _sr.color.b, _alpha);
        }
        else
        {
            _myColor = new Color(_sr.color.r, _sr.color.g, _sr.color.b, 1);
        }

        _sr.color = _myColor;
    }
}
