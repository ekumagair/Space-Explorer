using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int addScore = 0;
    public int upgrade = 0;
    public int addHealth = 0;
    public float offsetX = 0.5f;
    public float offsetY = 0f;
    public GameObject pickUpSound;

    private void Start()
    {
        transform.Translate(new Vector3(offsetX, offsetY, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StaticClass.score += addScore;

            if(addHealth > 0)
            {
                if(PlayerScript.hp >= 3)
                {
                    StaticClass.lives++;
                }

                PlayerScript.hp += addHealth;
            }
            
            if(PlayerScript.weaponUpgrade < upgrade)
            {
                PlayerScript.weaponUpgrade = upgrade;
            }

            Instantiate(pickUpSound, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
