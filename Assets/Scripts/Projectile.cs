using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int direction = 1;
    public bool vertical = false;
    public bool belongsToPlayer = false;

    private float _speedFactor = 1.0f;

    void Awake()
    {
        if (belongsToPlayer == false)
        {
            _speedFactor = StaticClass.enemySpeedMult;
        }
        else
        {
            _speedFactor = 1.0f;
        }

        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(0.05f);

        if (vertical == false)
        {
            transform.Translate(transform.right * speed * _speedFactor * direction);
        }
        else
        {
            transform.Translate(transform.up * speed * _speedFactor * direction);
        }

        StartCoroutine(Move());
    }
}
