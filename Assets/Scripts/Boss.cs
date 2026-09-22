using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int direction = -1;
    public GameObject explosion;
    public GameObject win;
    public GameObject shot;
    public GameObject shotSound;
    public GameObject deathSound;
    public float fireInterval = 1f;

    private GameObject _player;
    private bool _started = false;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(IsActive());
        _started = false;
    }

    void Update()
    {
        if (_started == true)
        {
            if (direction == -1 && transform.position.y < -3)
            {
                direction = 1;
            }
            if (direction == 1 && transform.position.y > 3)
            {
                direction = -1;
            }

            transform.Translate(5 * transform.up * direction * StaticClass.enemySpeedMult * Time.deltaTime);

            if (GetComponent<EnemyHealth>().health <= 0)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Instantiate(win, _player.transform.position, _player.transform.rotation);
                Instantiate(deathSound, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator IsActive()
    {
        yield return new WaitForSeconds(2.5f);

        StaticClass.lagObjs += 1;
        StartCoroutine(Shoot());
        _started = true;
    }

    private IEnumerator Shoot()
    {
        Instantiate(shotSound, transform.position, transform.rotation);

        var shotI = Instantiate(shot, transform.position, transform.rotation);
        shotI.GetComponent<Projectile>().direction = -1;

        yield return new WaitForSeconds(fireInterval / StaticClass.enemySpeedMult);

        StartCoroutine(Shoot());
    }

    void OnDestroy()
    {
        if (StaticClass.lagObjs > 0)
        {
            StaticClass.lagObjs -= 1;
        }
    }
}
