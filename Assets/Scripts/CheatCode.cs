using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatCode : MonoBehaviour
{
    public bool cheat_extraLives = false;
    public bool cheat_continue = false;
    public bool cheat_weapon = false;
    public bool once = true;
    public bool playSound = false;
    public KeyCode[] buttons;
    public int currentButton;

    void Start()
    {
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

            if(buttons[currentButton] == e.keyCode)
            {
                currentButton++;

                if(currentButton == buttons.Length)
                {
                    if (StaticClass.debug == true)
                    {
                        Debug.Log("Finished code");
                    }

                    currentButton = 0;

                    if(cheat_extraLives == true)
                    {
                        StaticClass.lives = 30;
                    }
                    if(cheat_continue == true)
                    {
                        StaticClass.score = 0;
                        StaticClass.lives = 3;
                        SceneManager.LoadScene("Level" + StaticClass.currentLevel);
                    }
                    if(cheat_weapon == true)
                    {
                        PlayerScript.weaponUpgrade = 1;
                    }

                    if(playSound == true)
                    {
                        GetComponent<AudioSource>().Play();
                    }

                    if(once == true)
                    {
                        cheat_extraLives = false;
                        cheat_continue = false;
                        cheat_weapon = false;
                    }
                }
            }
            else
            {
                currentButton = 0;
            }
        }
    }
}
