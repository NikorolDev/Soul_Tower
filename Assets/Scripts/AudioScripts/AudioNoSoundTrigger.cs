using UnityEngine;

/// <summary>
/// This script is used so that no sound played in certain areas of the game.
/// This is to stop any conflict with areas with background audio and without background audio
/// </summary>
public class AudioNoSoundTrigger : MonoBehaviour
{
    //References
    private AudioManager m_audioManager; //Reference to the audio manager to use its functions
    private BoxCollider2D m_audioTrigger; //Reference to the box collider to check if the player has collided with the collider

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
            //Pause the Audio that was playing previous to entering the area and set the current track name to nothing
            m_audioManager.PauseAudioTrack(AudioTrigger.CurrentAudioTrackName);
            AudioTrigger.CurrentAudioTrackName = null;
        }
    }
}
