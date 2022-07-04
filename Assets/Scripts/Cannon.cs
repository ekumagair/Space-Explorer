using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject projectile;
    public int direction = -1;
    public GameObject shotSound;

    private void Start()
    {
        transform.Translate(new Vector3(0.5f, -0.5f, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
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

    IEnumerator Shoot(float t)
    {
        Debug.Log("CannonShot");

        yield return new WaitForSeconds(t / StaticClass.enemySpeedMult);

        var p = Instantiate(projectile, transform.position, transform.rotation);
        p.GetComponent<Projectile>().direction = direction;

        Instantiate(shotSound, transform.position, transform.rotation);

        StartCoroutine(Shoot(Random.Range(2, 4)));
    }
}
