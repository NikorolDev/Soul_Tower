using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script controls the pause menu and its Ui componentes
/// </summary>
public class PauseMenu : MonoBehaviour
{
    //Private references
    private AudioManager m_audioManager; //Reference to the audio manger script
    private PlayerData m_playerData; //Reference to the player data script
    private GameObject m_camera; //Reference to teh camera object
    
    //GameObject references
    public GameObject MainMenuUI; //Reference to the main menu
    public GameObject PauseMenuUI; //Reference to the pause menu
    public GameObject OptionsTab; //Reference to the options tab
    public GameObject GiveUpMessageBox; //Reference to the give up message box

    // Start is called before the first frame update
    void Start()
    {
        //Grab reference to the audio manager, player data scripts and camera
        m_audioManager = FindObjectOfType<AudioManager>();
        m_playerData = FindObjectOfType<PlayerData>();
        m_camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        //if the player presses escape (Xbox: Start) and check if the is not in the main menu, outro squence and dialogue
        if (Input.GetButtonDown("Cancel") && MainMenu.InMainMenu == false && OutroSequence.InOutroSequence == false && InteractionDialogue.InDialogue == false)
        {
            //if the game is not paused
            if (PausingGame.IsGamePaused == false)
            {
                //Show the pause menu, which pause the game and puase the background audio
                DisplayPauseMenu();
                m_audioManager.PauseAudioTrack(AudioTrigger.CurrentAudioTrackName);
                
            }
            else
            {
                //Hide the pause, which resume the game and the background audio
                HidePauseMenu();
                m_audioManager.PlayAudioTrack(AudioTrigger.CurrentAudioTrackName);
                
            }
        }
    }

    //This function will pause the game, disable controls and display the pause menu
    private void DisplayPauseMenu()
    {
        PausingGame.PauseGame();
        PlayerController.ControlsEnabled = false;
        PauseMenuUI.SetActive(true);
    }

    //This function will resume the game, enables controls, closes all tabs and hides the pause menu
    private void HidePauseMenu()
    {
        PausingGame.ResumeGame();
        PlayerController.ControlsEnabled = true;
        PauseMenuUI.SetActive(false);
        CloseTabs();
    }

    //This function will close all tabs and show the options menu
    public void DisplayOptionsTab()
    {
        CloseTabs();
        OptionsTab.SetActive(true);
    }

    //This function will save game
    public void SaveGame()
    {
        //Close all tabs ad save the data
        CloseTabs();
        m_playerData.SaveGame();

        m_camera.transform.position = new Vector3(0, -10, -10); //Set the camera position
        MainMenu.InMainMenu = true; //Player is main menu
        PlayerController.ControlsEnabled = false; //Disable controls

        //Show main menu and hide pause menu
        MainMenuUI.SetActive(true);
        PauseMenuUI.SetActive(false);

        //Set the game saved case to 1 to show that there is saved data and play the main menu music
        PlayerPrefs.SetInt("Game Saved", MainMenu.HasGameSaved = 1);
        m_audioManager.PlayAudioTrack("Main Menu Music");
    }

    //This function will close all tabs and show the give up message box
    public void GiveUp()
    {
        CloseTabs();
        GiveUpMessageBox.SetActive(true);
    }

    //This function will delete the save and transfers player to the main menu
    public void YesGiveUp()
    {
        //Hide the message box and delete save
        GiveUpMessageBox.SetActive(false);
        m_playerData.DeleteSave();

        //Transfer player to the main menu
        m_camera.transform.position = new Vector3(0, -10, -10);
        MainMenu.InMainMenu = true;
        PlayerController.ControlsEnabled = false;
        PauseMenuUI.SetActive(false);
        MainMenuUI.SetActive(true);

        //Set the game save case to no data and set the attempt number and play the main menu music
        PlayerPrefs.SetInt("Game Saved", MainMenu.HasGameSaved = 0);
        PlayerPrefs.SetInt("Attempt Number", PlayerData.AttemptNumber);
        m_audioManager.PlayAudioTrack("Main Menu Music");
    }

    //This function will close all tabs accessed in the pause menu
    public void CloseTabs()
    {
        OptionsTab.SetActive(false);
        GiveUpMessageBox.SetActive(false);
    }
}
