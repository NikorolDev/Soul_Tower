using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is for interacting with the character's in the game
/// Reference: Brackeys. (2017). How to make a Dialogue System in Unity [online]. Available: https://www.youtube.com/watch?v=_nRzoTzeyxU [Last Accessed 20th April 2020].
/// </summary>
public class InteractionDialogue : MonoBehaviour
{
    //Game object reference, so that the dialogue box can be siplayed
    public GameObject DialogueBox;

    //Static bool to use as indicator for the pause menu to not be displayed
    public static bool InDialogue;

    //Reference to the dialogue data script to pass it to the dialogue manager
    public DialogueData Data;

    //When the player enters the collsion box, it will update the dialogue box
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Pass on the dialogue data to the corresponding character. This to avoid the wrong dialogue being used.
            FindObjectOfType<DialogueManager>().SetDialogue(Data);
        }
    }

    // Fixed Update is called every fixed frame
    void FixedUpdate()
    {
        //If the player presses an interaction button (E on keyboard or X on Xbox) and they can interact
        if (Input.GetButton("Interact") && InteractionChecker.CanInteract == true)
        {
            PausingGame.PauseGame();//Pause the game, so that the stopwatch is paused
            PlayerController.ControlsEnabled = false; //Disable controls
            InDialogue = true; //Set In dialogue to true
            DialogueBox.SetActive(true); //Display the dilaogue box
        }
    }
}
