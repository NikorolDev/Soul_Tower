using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script controls the outro sequence
/// </summary>
public class OutroSequence : MonoBehaviour
{
    //Private references
    private Animator m_animationOutroSequence; //Reference to the animator for the outro sequence
    private AudioManager m_audioManager;
    private GameObject m_camera; //Reference to the camera

    //Static bool to check if the player is in outro sequence
    public static bool InOutroSequence;

    //Game object references
    public GameObject MainMenuUI; //Reference to the main menu
    public GameObject OutroScreen; //Reference to the outro screen

    // Start is called before the first frame update
    void Start()
    {
        //Get reference to the animator, audio manager and camrea, and play the outro sequence animation
        m_animationOutroSequence = GetComponent<Animator>();
        m_audioManager = FindObjectOfType<AudioManager>();
        m_camera = GameObject.Find("Main Camera");
        m_animationOutroSequence.Play("OutroSequence", -1, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the animation has finished
        if(m_animationOutroSequence.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            //Stop the final stage music and play the main menu music
            m_audioManager.StopAudioTrack("Tower Of The Fallen Theme");
            m_audioManager.PlayAudioTrack("Main Menu Music");

            
            m_camera.transform.position = new Vector3(0, -10, -10);//Set the camera position to the where the main menu area
            PausingGame.PauseGame(); //Pause the game
            InOutroSequence = false; //Player is not in ourto sequence
            MainMenu.InMainMenu = true; //Player is in Mian Menu

            //Show the main menu and hide the outro sequence
            MainMenuUI.SetActive(true); 
            OutroScreen.SetActive(false);


        }
    }
}
