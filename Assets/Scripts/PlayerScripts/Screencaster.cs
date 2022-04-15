using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will display the screencaster and detect which keys are pressed to highlight the corresponding keys
/// </summary>
public class Screencaster : MonoBehaviour
{
    //Game object references to screencaster area and activated keys
    public GameObject ScreencasterArea, LeftArrowActive, SpacebarActive, RightArrowActive;

    //This function will display the screencaster and it is based on the toggle button
    public void DisplayScreencater(bool screencasterEnabled)
    {
        ScreencasterArea.SetActive(screencasterEnabled);
    }

    // Update is called once per frame
    void Update()
    {
        //if the player is moving right it will only highlight the right arrow key
        if (PlayerController.MoveInput > 0)
        { 
            LeftArrowActive.SetActive(false);
            RightArrowActive.SetActive(true);
        }
        //If the player is moving left it will only highlight the left arrow key
        else if (PlayerController.MoveInput < 0)
        {
            LeftArrowActive.SetActive(true);
            RightArrowActive.SetActive(false);
        }
        //if the player is idle it will not highlight any buttons
        else
        {
            LeftArrowActive.SetActive(false);
            RightArrowActive.SetActive(false);
        }

        //if the player is charging jump it will hightlight the spave bar
        if (Input.GetButton("Jump"))
        {
            SpacebarActive.SetActive(true);
        }

        //if the player jumps disable highlighted space bar.
        if (Input.GetButtonUp("Jump"))
        {
            SpacebarActive.SetActive(false);
        }
        
    }
}
