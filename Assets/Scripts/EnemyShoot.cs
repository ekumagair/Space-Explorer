using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public bool active = false;
    public bool hasShootAnimation = true;
    public float frequency;
    public float attackDuration;
    public bool alwaysShootDown = false;
    public GameObject shot;
    public GameObject shotSound;
    EnemyWalk walkScript;
    Animator _animator;

    void Start()
    {
        if (GetComponent<EnemyWalk>() != null)
        {
            walkScript = GetComponent<EnemyWalk>();
        }
        if (GetComponent<Animator>() != null)
        {
            _animator = GetComponent<Animator>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainCamera" && active == false)
        {
            active = true;
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        if (_animator != null && hasShootAnimation == true)
        {
            _animator.SetBool("Shooting", true);
        }

        Instantiate(shotSound, transform.position, transform.rotation);
        var shotI = Instantiate(shot, transform.position, transform.rotation);

        if (alwaysShootDown == false)
        {
            if (walkScript != null)
            {
                shotI.GetComponent<Projectile>().direction = walkScript.direction;
            }
            else
            {
                shotI.GetComponent<Projectile>().direction = -1;
            }
        }
        else
        {
            shotI.GetComponent<Projectile>().vertical = true;
            shotI.GetComponent<Projectile>().direction = -1;
        }

        yield return new WaitForSeconds(attackDuration / StaticClass.enemySpeedMult);

        if (_animator != null && hasShootAnimation == true)
        {
            _animator.SetBool("Shooting", false);
        }

        yield return new WaitForSeconds(frequency / StaticClass.enemySpeedMult);

        StartCoroutine(Shoot());
    }
}
