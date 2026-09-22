using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CheatCode : MonoBehaviour
{
    [Header("Properties")]
    public bool once = true;
    public bool playSound = false;

    [Header("Input")]
    public KeyCode[] buttons;
    public int currentButton;

    [Header("Cheat Effects")]
    public bool cheat_extraLives = false;
    public bool cheat_continue = false;
    public bool cheat_weapon = false;
    public UnityEvent onCheatTyped;

    private AudioSource _as;

    void Start()
    {
        _as = GetComponent<AudioSource>();

        currentButton = 0;
    }

    void OnGUI()
    {
        Event e = Event.current;

        if (e.isKey && Event.current.type == EventType.KeyUp)
        {
            if (StaticClass.debug == true)
            {
                Debug.Log("Pressed " + e.keyCode);
            }

            if (buttons[currentButton] == e.keyCode)
            {
                // Check next key.
                currentButton++;

                // If typed every key.
                if (currentButton == buttons.Length)
                {
                    if (StaticClass.debug == true)
                    {
                        Debug.Log("Finished code");
                    }

                    ExecuteEffect();
                }
            }
            else
            {
                currentButton = 0;
            }
        }
    }

    private void ExecuteEffect()
    {
        currentButton = 0;

        if (cheat_extraLives == true)
        {
            StaticClass.lives = 30;
        }
        if (cheat_continue == true)
        {
            StaticClass.score = 0;
            StaticClass.lives = 3;
            SceneManager.LoadScene("Level" + StaticClass.currentLevel.ToString());
        }
        if (cheat_weapon == true)
        {
            PlayerScript.weaponUpgrade = 1;
        }

        onCheatTyped?.Invoke();

        if (playSound == true)
        {
            if (_as == null)
            {
                _as = GetComponent<AudioSource>();
            }

            _as.Play();
        }

        if (once == true)
        {
            cheat_extraLives = false;
            cheat_continue = false;
            cheat_weapon = false;
        }
    }
}
