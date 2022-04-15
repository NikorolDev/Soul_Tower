using UnityEngine;

/// <summary>
/// This script is only used to store data on audio on each audio that will be used in game. They will entered and manipulated in the Inspector.
/// This will make it easier to control the audio.
/// Reference: Brackeys. (2017). Introduction to AUDIO in Unity [online]. Available: https://www.youtube.com/watch?v=6OT43pvUyfY [Last Accessed 28th April 2020].
/// </summary>
[System.Serializable]
public class AudioList
{
    //Audio Data properties
    public string AudioName; //Name of the sound
    public AudioClip AudioTrack; //File of the sound

    [Range(0f, 1f)]
    public float AudioVolume; //Audio volume

    public bool AudioLoop; //Enabling looping of the audio

    [HideInInspector]
    public AudioSource AudioSource; //AudioSource of the audio that will be played
}
