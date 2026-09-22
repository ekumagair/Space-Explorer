using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashText : MonoBehaviour
{
    public float time;

    private Text _text;

    void Start()
    {
        _text = GetComponent<Text>();
        StartCoroutine(Flash(time));
    }

    private IEnumerator Flash(float t)
    {
        yield return new WaitForSeconds(t);

        _text.enabled = !_text.enabled;
        StartCoroutine(Flash(t));
    }
}
