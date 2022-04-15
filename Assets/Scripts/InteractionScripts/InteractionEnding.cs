using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is for interacting with a character that will display the ending
/// </summary>
public class InteractionEnding : MonoBehaviour
{
    //Game object reference, so that the outro screen can be displayed
    public GameObject OutroScreen;

    //When the player enters the collision box
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("Game Saved", MainMenu.HasGameSaved = 2); //Set the game as completed
            OutroSequence.InOutroSequence = true; //Indicator to tell the player is in the outro sequence
            PlayerController.ControlsEnabled = false; //Disable player controls
            FindObjectOfType<PlayerData>().SaveGame(); //Save the game
            OutroScreen.SetActive(true); //Display the Outro screen.
        }
    }
}
