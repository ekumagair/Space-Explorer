using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public GameObject scrollCenter;
    public GameObject scrollTarget;
    public float limitX = 200;

    void Start()
    {
        scrollTarget = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if(scrollTarget.transform.position.x > scrollCenter.transform.position.x && transform.position.x < limitX)
        {
            transform.position = new Vector3(scrollTarget.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
