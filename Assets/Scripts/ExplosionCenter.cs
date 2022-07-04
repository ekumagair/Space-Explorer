using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCenter : MonoBehaviour
{
    public GameObject explosion;

    private void Awake()
    {
        var e1 = Instantiate(explosion, transform.position, transform.rotation);
        e1.GetComponent<Explosion>().directionX = 1;
        e1.GetComponent<Explosion>().directionY = 1;

        var e2 = Instantiate(explosion, transform.position, transform.rotation);
        e2.GetComponent<Explosion>().directionX = -1;
        e2.GetComponent<Explosion>().directionY = 1;

        var e3 = Instantiate(explosion, transform.position, transform.rotation);
        e3.GetComponent<Explosion>().directionX = 1;
        e3.GetComponent<Explosion>().directionY = -1;

        var e4 = Instantiate(explosion, transform.position, transform.rotation);
        e4.GetComponent<Explosion>().directionX = -1;
        e4.GetComponent<Explosion>().directionY = -1;

        Destroy(gameObject);
    }
}
