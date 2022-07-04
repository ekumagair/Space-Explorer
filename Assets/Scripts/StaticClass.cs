using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticClass
{
    // Layers:
    // Solid = Solid to everything.
    // SolidPlayer = Only solid to the player.
    // Character = Characters that aren't the player.
    // CharacterPlayer = The player.
    // EnemyTrigger = Enemy colliders that only hit the player.
    // IgnoredByCharacters = Doesn't collide with characters.

    public static int lagObjs = 0;
    public static int lagLevel = 0;

    public static int score = 0;
    public static int highScore = 0;
    public static int currentLevel = 1;
    public static int lives = 3;

    public static bool passedCheckpoint = false;
    public static float checkpointX = 0;
    public static float checkpointY = 0;

    public static bool hardMode = false;
    public static float enemySpeedMult = 1.0f;

    public static bool debug = false;
}