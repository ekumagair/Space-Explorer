using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashText : MonoBehaviour
{
    public float time;
    Text txt;

    void Start()
    {
        txt = GetComponent<Text>();
        StartCoroutine(Flash(time));
    }

    IEnumerator Flash(float t)
    {
        yield return new WaitForSeconds(t);
        txt.enabled = !txt.enabled;
        StartCoroutine(Flash(t));
    }
}
