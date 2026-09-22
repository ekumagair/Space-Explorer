using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour
{
    public float speed;
    public float offsetY = -0.5f;

    private bool _active = false;

    void Start()
    {
        _active = false;
        transform.Translate(new Vector3(0, offsetY, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            _active = true;
        }
        else if (collision.gameObject.tag == "BarrierLeft")
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (_active)
        {
            transform.Translate(-transform.right * speed * StaticClass.enemySpeedMult * Time.deltaTime);
        }
    }
}
