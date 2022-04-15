using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This script will display the Character's name in the dialogue box. The reason it is on it's own script, becuase it can't be accessed whilst being inactive
/// </summary>
public class DisplayCharacterName : MonoBehaviour
{
    //Private text mesh pro in order to display the text
    private TextMeshProUGUI m_textCharacterName;

    // Start is called before the first frame update
    void Start()
    {
        //Grab a reference of the text mesh pro component
        m_textCharacterName = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //Display character's name from the dialogue manager
        m_textCharacterName.text = DialogueManager.TextCharacterName; 
    }
}
