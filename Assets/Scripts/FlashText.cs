using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashText : MonoBehaviour
{
    public float time;
    private Text txt;

    void Start()
    {
        txt = GetComponent<Text>();
        StartCoroutine(Flash(time));
    }

    private IEnumerator Flash(float t)
    {
        yield return new WaitForSeconds(t);

        txt.enabled = !txt.enabled;
        StartCoroutine(Flash(t));
    }
}
