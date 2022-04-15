using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This script controls how the location names are displayed
/// </summary>
public class LocationDisplay : MonoBehaviour
{
    //Private references
    private Animator m_textAnimationFadeOut; //Reference to the animator to play the animation
    private GameObject m_camera; //Reference to the camera
    private TextMeshProUGUI m_textLocation; //Reference to the text mesh pro to dispaly text

    //Static integer to determine in what area the player is
    public static int LocationID; 

    // Start is called before the first frame update
    void Start()
    {
        //Grab references to theanimator, camrea, and text mesh pro
        m_textAnimationFadeOut = GetComponent<Animator>();
        m_camera = GameObject.Find("Main Camera");
        m_textLocation = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //Constantly check the position of the camrea to display the location name
        SwitchingLocationText();
    }

    //This function will display the name of the location
    private void SwitchingLocationText()
    {
        //First check if the player is not in the main menu
        if (MainMenu.InMainMenu == false)
        {
            //Display location name based on location ID
            switch (LocationID)
            {
                case 0: //Deadwoods
                    //First set the text
                    m_textLocation.text = "- Deadwoods -";

                    //Check if the camera position is above or on this position. This is useful for when the player continues from their previous save
                    if (m_camera.transform.position.y >= 16.3f)
                    {
                        //Switch location ID and play the animation
                        LocationID = 1;
                        m_textAnimationFadeOut.Play("TextFadeOut", -1, 0f);
                    }
                    break;

                case 1: //Drains of Trenthelm
                    //Set it to this text
                    m_textLocation.text = "- Drains of Trenthelm -";

                    if (m_camera.transform.position.y >= 40.9f)
                    {
                        LocationID = 2;
                        m_textAnimationFadeOut.Play("TextFadeOut", -1, 0f);
                    }
                    break;

                case 2: //Trenthelm's Front Gate
                    m_textLocation.text = "- Trenthelm's Front Gate -";

                    if (m_camera.transform.position.y >= 57.3f)
                    {
                        LocationID = 3;
                        m_textAnimationFadeOut.Play("TextFadeOut", -1, 0f);
                    }
                    break;

                case 3: //Helm's Peaks
                    m_textLocation.text = "- Helm's Peaks -";

                    if(m_camera.transform.position.y >= 73.7f)
                    {
                        LocationID = 4;
                        m_textAnimationFadeOut.Play("TextFadeOut", -1, 0f);
                    }
                    break;

                case 4: //Tower of the Fallen
                    m_textLocation.text = "- Tower of the Fallen -";

                    if(m_camera.transform.position.y >= 90.1f)
                    {
                        LocationID = 5;
                        m_textAnimationFadeOut.Play("TextFadeOut", -1, 0f);
                    }
                    break;

                case 5: //Giovanni's Lair
                    m_textLocation.text = "- Giovanni's Lair";
                    break;
            }
        }
        else
        {
            //This will play once the player is out of the main menu. It will not play instantly as the game will be frozen
            m_textAnimationFadeOut.Play("TextFadeOut", -1, 0f);
        }
    }
}
