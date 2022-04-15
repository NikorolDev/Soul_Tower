using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This script controls how the jump counter is displayed
/// </summary>
public class JumpCounter : MonoBehaviour
{
    private TextMeshProUGUI m_textJumpCounter;

    // Start is called before the first frame update
    void Start()
    {
        m_textJumpCounter = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame. These methods have been used throughout text displaying scripts
    void Update()
    {
        //Display the Jump counter
        m_textJumpCounter.text = "Jumps: " + PlayerData.JumpCounter;

        //This will display the amount of jumps in the main menu's saved data area
        if (MainMenu.InMainMenu == true)
        {
            m_textJumpCounter.text = "Jumps: " + PlayerPrefs.GetInt("Jumps");
        }

        //This will display the amount of jumps made to complete the game
        if(OutroSequence.InOutroSequence == true)
        {
            m_textJumpCounter.text = "Jumps: " + PlayerData.JumpCounter;
        }
    }
}
