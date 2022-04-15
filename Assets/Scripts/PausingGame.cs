using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is for setting the game timescale in order to pause or resume the game.
/// The reason it is in a seperate script is becuase it can be easily controlled.
/// </summary>
public class PausingGame : MonoBehaviour
{
    //Static bool property to check if the game is paused
    public static bool IsGamePaused;

    //This static function will freeze the game and set pause to true
    public static void PauseGame()
    {
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    //This static function will unfreeze the game and set pause to false
    public static void ResumeGame()
    {
        Time.timeScale = 1f;
        IsGamePaused = false;
    }
}
