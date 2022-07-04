using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int direction = 1;
    public bool vertical = false;
    public bool belongsToPlayer = false;
    float mult = 1.0f;

    void Awake()
    {
        if (belongsToPlayer == false)
        {
            mult = StaticClass.enemySpeedMult;
        }
        else
        {
            mult = 1.0f;
        }

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(0.05f);

        if (vertical == false)
        {
            transform.Translate(transform.right * speed * mult * direction);
        }
        else
        {
            transform.Translate(transform.up * speed * mult * direction);
        }

        StartCoroutine(Move());
    }
}
