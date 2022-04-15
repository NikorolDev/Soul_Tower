using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script controls the intro sequence
/// </summary>
public class IntroSequence : MonoBehaviour
{
    //Private reference to the animator to play the intro sequence animation
    private Animator m_textAnimationIntroSequence;

    //Game object reference to the intro screen
    public GameObject IntroScreen;

    // Start is called before the first frame update
    void Start()
    {
        //Get reference to the animator and play the intro animation. This will be played everytime the intro sequence is active
        m_textAnimationIntroSequence = GetComponent<Animator>();
        m_textAnimationIntroSequence.Play("IntroSequence", -1, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //check if the animation has finished
        if (m_textAnimationIntroSequence.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        { 
            FindObjectOfType<AudioManager>().StopAudioTrack("Main Menu Music"); //Stop the main menu music        
            IntroScreen.SetActive(false); //Hide the intro sequence scrren
            MainMenu.InMainMenu = false; //Player is not in maine menu anymore
            PlayerController.ControlsEnabled = true; //Enable the controls
        }
    }
}
