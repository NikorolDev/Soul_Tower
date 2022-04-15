using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This script manages how the dialogue starts, changes and ends.
/// Reference: Brackeys. (2017). How to make a Dialogue System in Unity [online]. Available: https://www.youtube.com/watch?v=_nRzoTzeyxU [Last Accessed 20th April 2020].
/// </summary>
public class DialogueManager : MonoBehaviour
{
    //private queue that gets a list of dialogue to display
    private Queue<string> m_dialogues;

    //Static feilds
    //Static strings
    public static string TextCharacterName; //Name of the NPC interacting
    public static string TextDialogue; //NPC's dialogue

    //Game object reference to the dialogue box
    public GameObject DialogueBox;

    // Start is called before the first frame update
    void Start()
    {
        //Start a new queue, which will start the dialogue from the beginning
        m_dialogues = new Queue<string>();
    }

    //This function will set the dialogue that passes the dialogue data from each character
    public void SetDialogue(DialogueData dialogueData)
    {
        TextCharacterName = dialogueData.CharacterName; //Display the character's name 
        m_dialogues.Clear(); //Clear everything in the text dialogue area

        //Gets every dialogue in the array and adds it to the queue list, like an array
        foreach(string dialogue in dialogueData.Dialogue)
        {
            m_dialogues.Enqueue(dialogue);
        }

        NextDialogue(); //Display the first dialogue in the queue
    }

    //This function will load the next dialogue in the queue, which will be controlled by the dialogue box's "Next" button
    public void NextDialogue()
    {
        //if there isn't anymore dialogue, then
        if(m_dialogues.Count == 0)
        {
            //End dialogue, by closing it
            EndDialogue();
        }
        else //However, if there's more dialogue, then
        {
            //remove the dialogue from the queue and store it in a local string and display it on the screen
            string dialogue = m_dialogues.Dequeue();
            TextDialogue = dialogue;
        }
    }

    //This function will close the dialogue box and resume the game
    public void EndDialogue()
    {
        PausingGame.ResumeGame();
        InteractionDialogue.InDialogue = false;
        PlayerController.ControlsEnabled = true;
        DialogueBox.SetActive(false);
    }
}
