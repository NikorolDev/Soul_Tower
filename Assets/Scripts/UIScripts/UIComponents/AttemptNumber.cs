using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This script controls how the attempts are displayed
/// </summary>
public class AttemptNumber : MonoBehaviour
{
    //Reference to the text mesh pro to change the text. This is used throughout all text displaying scripts
    private TextMeshProUGUI m_textAttemptNumber;

    // Start is called before the first frame update
    void Start()
    {
        //Grab reference to the text mesh pro. This is used throughout all text displaying scripts
        m_textAttemptNumber = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //Display the attempts made
        m_textAttemptNumber.text = "Attempt: " + PlayerData.AttemptNumber;
    }
}
