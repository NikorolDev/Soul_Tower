using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This script controls how the session number is displayed
/// </summary>
public class SessionNumber : MonoBehaviour
{
    private TextMeshProUGUI m_textSessionNumber;

    // Start is called before the first frame update
    void Start()
    {
        m_textSessionNumber = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    { 
        //Display the seesion number
        m_textSessionNumber.text = "Session: " + PlayerData.SessionNumber;

        if (MainMenu.InMainMenu == true)
        {
            m_textSessionNumber.text = "Session: " + PlayerPrefs.GetInt("Session Number");
        }
        
        if(OutroSequence.InOutroSequence == true)
        {
            m_textSessionNumber.text = "Completed On Attempt " + PlayerData.AttemptNumber + " - Session " + PlayerData.SessionNumber;
        }
    }
}
