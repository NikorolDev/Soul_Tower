using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This script will display the dialogue in the dialogue box. The reason it is on it's own script, becuase it can't be accessed whilst being inactive
/// </summary>
public class DisplayDialogue : MonoBehaviour
{
    //Private text mesh pro in order to display the text
    private TextMeshProUGUI m_textDialogue;

    // Start is called before the first frame update
    void Start()
    {
        //Grab a reference of the text mesh pro component
        m_textDialogue = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //Display the dialogue from the dialogue manager
        m_textDialogue.text = DialogueManager.TextDialogue;
    }
}
