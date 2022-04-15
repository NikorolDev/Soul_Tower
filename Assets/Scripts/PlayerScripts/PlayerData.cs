using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Script is to store all data about the player, this will avoid the amount of times needed to reference these game objects and will be used for saving and retrieving those values using PlayerPrefs
/// </summary>
public class PlayerData : MonoBehaviour
{
    //Game object feilds. They will be used to get values needed, such as positions
    private GameObject m_player;
    private GameObject m_camera; 

    //Static feilds
    //Static integers
    public static int AttemptNumber; //Number of attempts, which the game will count the amount of attempts the player has done
    public static int SessionNumber; //Number of seesions in an attempt, which the game will the amount of sessions they continued from the attempt
    public static int JumpCounter; //Number of jumps in an attempt

    //Static floats
    public static float Stopwatch; //Time it took to complete an attempt
    public static float PlayerPositionX; //Player's X position
    public static float PlayerPositionY; //Player's Y position
    public static float CameraPositionY; //Camera's Y position

    //Start is called before the first frame update
    void Start()
    {
        //Get a reference of the player and camera.
        m_camera = GameObject.Find("Main Camera");
        m_player = GameObject.Find("PlayerSprite");
    }

    // Update is called once per frame
    void Update()
    {
        //When controls are enabled, start or resume the stopwatch
        if(PlayerController.ControlsEnabled == true)
        {
            Stopwatch += Time.deltaTime; //Use delta time to count the time
        }

        //Update player positions
        PlayerPositionX = m_player.transform.position.x;
        PlayerPositionY = m_player.transform.position.y;
        CameraPositionY = m_camera.transform.position.y;

        //Every time the player jumps increment jump counter by 1
        if (Input.GetButtonUp("Jump") && PlayerController.ControlsEnabled == true)
        {
            JumpCounter += 1;
        }
    }

    //This function will set a new game
    public void SetNewGame()
    {
        PlayerPrefs.SetInt("Attempt Number", AttemptNumber++); //Increment the attempt counter
        //Reset session number, stopwatch, jumpcounter and location id as it is a new game
        SessionNumber = 1;
        Stopwatch = 0;
        JumpCounter = 0;
        LocationDisplay.LocationID = 0;

        //Set spawn position for the player and camera
        m_camera.transform.position = new Vector3(0, -0.1f, -10);
        m_player.transform.position = new Vector3(-0.8f, -2.57f, 0);
    }

    //This function will retrieve all data that has been saved and set position of the camera and player from where it was saved and set location ID to 0
    public void GetSaveGame()
    {
        SessionNumber = PlayerPrefs.GetInt("Session Number") + 1;
        Stopwatch = PlayerPrefs.GetFloat("Session Time");
        JumpCounter = PlayerPrefs.GetInt("Jumps");
        m_player.transform.position = new Vector3(PlayerPrefs.GetFloat("Player Position X"), PlayerPrefs.GetFloat("Player Position Y"), 0);
        m_camera.transform.position = new Vector3(0, PlayerPrefs.GetFloat("Camera Position Y"), -10);
        LocationDisplay.LocationID = 0;
    }

    //This function will save data needed
    public void SaveGame()
    {
        PlayerPrefs.SetInt("Attempt Number", AttemptNumber);
        PlayerPrefs.SetInt("Session Number", SessionNumber);
        PlayerPrefs.SetFloat("Session Time", Stopwatch);
        PlayerPrefs.SetInt("Jumps", JumpCounter);
        PlayerPrefs.SetFloat("Player Position X", PlayerPositionX);
        PlayerPrefs.SetFloat("Player Position Y", PlayerPositionY);
        PlayerPrefs.SetFloat("Camera Position Y", CameraPositionY);
    }

    //This function will delete data and set the camere to where the main menu is located
    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey("Session Number");
        PlayerPrefs.DeleteKey("Session Time");
        PlayerPrefs.DeleteKey("Jumps");
        PlayerPrefs.DeleteKey("Player Position X");
        PlayerPrefs.DeleteKey("Player Position Y");
        PlayerPrefs.DeleteKey("Camera Position Y");

        m_camera.transform.position = new Vector3(0, -10, -10);
    }
}
