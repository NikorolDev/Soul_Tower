using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is only used to store data on dialogue on each character that will be interacted with. That will be entered in the engine's inspector
/// Reference: Brackeys. (2017). How to make a Dialogue System in Unity [online]. Available: https://www.youtube.com/watch?v=_nRzoTzeyxU [Last Accessed 20th April 2020].
/// </summary>
[System.Serializable]
public class DialogueData
{
    //Dialogue data properties
    public string CharacterName; //Character's name to display it in the dialogue box
    public string[] Dialogue; //Dialogue is stored in an array to display it in the dialogue box. This will allow to easily go through every dialogue
}
