using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is to trigger the audio to play based on the player being in the box collider.
/// </summary>
public class AudioTrigger : MonoBehaviour
{
    //References
    private AudioManager m_audioManager; //Reference to the audio manager to use its functions
    private BoxCollider2D m_audioTrigger; //Reference to the box collider to check if the player has collided with the collider

    //String variables
    public string AudioTrackName; //The name of the background audio to be played
    public static string CurrentAudioTrackName; //A copy of the name of the background audio. This is used in pause menu to pause the correct track

    // Start is called before the first frame update
    void Start()
    {
        //Grab references of the audio manager script and collider
        m_audioManager = FindObjectOfType<AudioManager>();
        m_audioTrigger = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the player is in the main menu and either disable or enable the collider
        if (MainMenu.InMainMenu == false)
        {
            m_audioTrigger.enabled = true;
        }
        else
        {
            m_audioTrigger.enabled = false;
        }
    }

    //When the player enters the collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Play the audio track for the area and set it as current track
            m_audioManager.PlayAudioTrack(AudioTrackName);
            CurrentAudioTrackName = AudioTrackName;
        }
        
    }

    //When the player leaves the collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Pause the audio track, so when the player returns to the area, it carry on from it left from
            m_audioManager.PauseAudioTrack(AudioTrackName);
        }
    }
}
