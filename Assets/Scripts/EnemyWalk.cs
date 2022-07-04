using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    public float speed;
    public int direction = -1;
    public bool active = false;
    public bool turnAroundLedges = true;
    public bool mirrorWhenTurning = false;
    bool turning;
    public LayerMask solidMask;

    SpriteRenderer _sr;
    Collider2D _collider;
    RaycastHit2D solidHit;
    bool wallLeft, wallRight = false;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        wallLeft = false;
        wallRight = false;
        turning = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            active = true;
        }
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.transform.parent.GetComponent<EnemyWalk>() != null && Time.timeScale > 0)
        {
            Turn();
            collision.gameObject.transform.parent.GetComponent<EnemyWalk>().Turn();
        }
    }

    void Update()
    {
        if (active && Time.timeScale > 0)
        {
            solidHit = Physics2D.BoxCast(transform.position, new Vector2(_collider.bounds.size.x, _collider.bounds.size.y * 0.65f), 0, transform.right, _collider.bounds.size.x / 4, solidMask);
            if(solidHit.collider == null)
            {
                wallRight = false;
            }
            else if (direction > 0)
            {
                wallRight = true;
            }

            solidHit = Physics2D.BoxCast(transform.position, new Vector2(_collider.bounds.size.x, _collider.bounds.size.y * 0.65f), 0, -transform.right, _collider.bounds.size.x / 4, solidMask);
            if (solidHit.collider == null)
            {
                wallLeft = false;
            }
            else if (direction < 0)
            {
                wallLeft = true;
            }

            if (wallLeft == false && wallRight == false)
            {
                transform.Translate(new Vector3(speed * direction * StaticClass.enemySpeedMult * Time.deltaTime, 0, 0));
            }
            else if(direction > 0 && wallRight)
            {
                Turn();
            }
            else if (direction < 0 && wallLeft)
            {
                Turn();
            }

            solidHit = Physics2D.Raycast(transform.position, -transform.up, _collider.bounds.size.y * 0.6f, solidMask);
            if (solidHit.collider == null && turnAroundLedges && wallLeft == false && wallRight == false)
            {
                Turn();
            }
        }
    }

    public void Turn()
    {
        if (StaticClass.debug == true)
        {
            Debug.Log("Turn");
        }

        if (turning == false && Time.timeScale > 0)
        {
            direction *= -1;
            transform.Translate(new Vector3(20 * direction * Time.deltaTime, 0, 0));
            StartCoroutine(TurnCoroutine());

            if (mirrorWhenTurning)
            {
                _sr.flipX = !_sr.flipX;
            }
        }
    }

    IEnumerator TurnCoroutine()
    {
        turning = true;

        yield return new WaitForSeconds(0.1f);

        turning = false;
    }
}
