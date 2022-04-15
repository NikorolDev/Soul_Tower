using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script checks if the player can interact with the NPCs in the game
/// </summary>
public class InteractionChecker : MonoBehaviour
{
    //This is to check if the player can interact with the NPC
    public static bool CanInteract;

    //2 speach bubbles that are used to indicate the character's the player can indicate
    public GameObject SpeachBubbleNormal; //This will be just an indictor, to show who the player can interact with
    public GameObject SpeackBubbleInteract; //This will show what button to press to interact

    //When the player enters the collsion box, it will show the interacting speach bubble
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpeachBubbleNormal.SetActive(false);
            SpeackBubbleInteract.SetActive(true);
            CanInteract = true;
        }
    }

    //When the player leaves the collision box, it will set the speach bubble back to normal
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpeachBubbleNormal.SetActive(true);
            SpeackBubbleInteract.SetActive(false);
            CanInteract = false;
        }
    }
}
