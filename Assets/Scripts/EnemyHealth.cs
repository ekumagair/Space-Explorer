using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 1;
    public int value = 100;
    public bool destroy = true;

    public GameObject deathSoundObj;
    public GameObject damageSoundObj;
    GameObject lastShot;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerShot" && Time.timeScale > 0)
        {
            if (StaticClass.debug == true)
            {
                Debug.Log("Hit");
            }

            if (collision.gameObject != lastShot || lastShot == null)
            {
                if (PlayerScript.weaponUpgrade == 0)
                {
                    health -= 1;
                    Destroy(collision.gameObject);
                }
                else if (PlayerScript.weaponUpgrade == 1)
                {
                    health -= 2;
                }

                if (health <= 0 && destroy)
                {
                    Instantiate(deathSoundObj, transform.position, transform.rotation);
                    Destroy(gameObject);
                    StaticClass.score += value;
                }
                else
                {
                    Instantiate(damageSoundObj, transform.position, transform.rotation);
                }

                lastShot = gameObject;
            }
        }
    }
}
