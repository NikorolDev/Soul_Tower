using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script controls the main menu UI components
/// </summary>
public class MainMenu : MonoBehaviour
{
    //Reference to the player data script
    private PlayerData m_playerData;

    //Static feilds
    public static int HasGameSaved; //This is to check if the player has saved or completed the game
    public static bool InMainMenu; //This is to check if the player is in main menu

    //Game Object references
    public GameObject MainMenuUI; //Reference to the main menu
    public GameObject SavedDataArea; //Refereence to the saved data area
    public GameObject NoSaveMessageBox; //Reference to the no save message box
    public GameObject SaveFoundMessageBox; //Reference to the save found message box
    public GameObject GameCompletedMessageBox; //Reference to the game completed message box
    public GameObject OptionsTab; //Reference to options tab
    public GameObject CreditsTab; //Reference to credits tab
    public GameObject IntroScreen; //Reference to intro screen

    // Start is called before the first frame update
    void Start()
    {
        m_playerData = FindObjectOfType<PlayerData>(); //Grab reference of theplayer data
        FindObjectOfType<AudioManager>().PlayAudioTrack("Main Menu Music"); //Play main menu music
        PlayerData.AttemptNumber = PlayerPrefs.GetInt("Attempt Number"); //Get saved attempt number 
        HasGameSaved = PlayerPrefs.GetInt("Game Saved"); //Get game saved case
        PausingGame.PauseGame(); //Freeze game as it's paused
        InMainMenu = true; //The player is in Main Menu
    }

    //This function will set up a new game
    private void SettingNewGame()
    {
        PlayerPrefs.SetInt("Game Saved", HasGameSaved = 0); //Set the case to no data
        m_playerData.DeleteSave(); //Delete all the data
        m_playerData.SetNewGame(); //And set new data

        //Hide main menu and show the intro sequence and unfreeze the game
        MainMenuUI.SetActive(false);
        IntroScreen.SetActive(true);
        PausingGame.ResumeGame();
    }

    //This function will start new game based on the game saved case
    public void StartNewGame()
    {
        //Close all tabs
        CloseTabs();

        //Based on the game save
        switch (PlayerPrefs.GetInt("Game Saved"))
        {
            //Set new game
            case 0:
                SettingNewGame();
                CloseTabs();
                break;

            //Game save found display message box
            case 1:
            case 2:
                SaveFoundMessageBox.SetActive(true);
                break;
        }
    }

    //This function will confirm a new game
    public void ConfirmNewGame()
    {
        SettingNewGame();
        CloseTabs();
    }

    //This function will continue the game
    public void ContinueGame()
    {
        //Based on game saved
        switch (PlayerPrefs.GetInt("Game Saved"))
        {
            //No save game found, display message box
            case 0:
                CloseTabs();
                DisplayNoSaveMessageBox();
                break;

                //Save found, get save game, hide main menu and show intro sequence and unfreeze game
            case 1:
                m_playerData.GetSaveGame();
                CloseTabs();

                MainMenuUI.SetActive(false);
                
                IntroScreen.SetActive(true);
                PausingGame.ResumeGame();
                break;

                //Game has been completed, show message box
            case 2:
                CloseTabs();
                GameCompletedMessageBox.SetActive(true);
                break;
        }
    }

    //This function will display options tab and close other tabs
    public void DisplayOptionsTab()
    {
        CloseTabs();
        OptionsTab.SetActive(true);
    }

    //This function will display credits tab and close other tabs
    public void DisplayCreditsTab()
    {
        CloseTabs();
        CreditsTab.SetActive(true);
    }    
    
    //This function will display the no save message box and close other tabs
    public void DisplayNoSaveMessageBox()
    {
        CloseTabs();
        NoSaveMessageBox.SetActive(true);
    }

    //This function will close all tabs even if they're already closed
    public void CloseTabs()
    {
        NoSaveMessageBox.SetActive(false);
        SaveFoundMessageBox.SetActive(false);
        GameCompletedMessageBox.SetActive(false);
        OptionsTab.SetActive(false);
        CreditsTab.SetActive(false);
    }

    //This function will quit the application
    public void QuitGame()
    {
        Application.Quit();
    }    

    // Update is called once per frame
    void Update()
    {
        //Check if a game attempt has been made, which will determine if the saved data area will be displayed
        if (PlayerData.AttemptNumber < 1)
        {
            SavedDataArea.SetActive(false);
        }
        else
        {
            SavedDataArea.SetActive(true);
        }
    }
}
