using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutsideOfCamera : MonoBehaviour
{
    public bool active = true;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "MainCamera" && active)
        {
            Destroy(gameObject);
        }
    }
}
