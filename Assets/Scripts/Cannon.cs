using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject projectile;
    public int direction = -1;
    public GameObject shotSound;

    void Start()
    {
        transform.Translate(new Vector3(0.5f, -0.5f, 0));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            StartCoroutine(Shoot(Random.Range(1, 4)));
        }
        else if (collision.gameObject.tag == "BarrierLeft")
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Shoot(float t)
    {
        if (StaticClass.debug == true)
        {
            Debug.Log("CannonShot");
        }

        yield return new WaitForSeconds(t / StaticClass.enemySpeedMult);

        var p = Instantiate(projectile, transform.position, transform.rotation);
        p.GetComponent<Projectile>().direction = direction;

        Instantiate(shotSound, transform.position, transform.rotation);

        StartCoroutine(Shoot(Random.Range(2, 4)));
    }
}
