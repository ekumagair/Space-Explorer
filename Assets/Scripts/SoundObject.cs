using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, GetComponent<AudioSource>().clip.length);
    }
}
