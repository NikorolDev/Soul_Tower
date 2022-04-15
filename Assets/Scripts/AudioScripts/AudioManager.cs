using System;
using UnityEngine;

/// <summary>
/// This script will manage the audio in the game. It will allow the audio to be played, paused and stopped
/// Reference: Brackeys. (2017). Introduction to AUDIO in Unity [online]. Available: https://www.youtube.com/watch?v=6OT43pvUyfY [Last Accessed 28th April 2020].
/// </summary>
public class AudioManager : MonoBehaviour
{
    //This grabs every audio and its data as an array
    public AudioList[] AudioTracks;
    
    //On awake get every audio stored and set the audio file, volume, and looping into the audio source.
    void Awake()
    {
        foreach(AudioList audioTrack in AudioTracks)
        {
            audioTrack.AudioSource = gameObject.AddComponent<AudioSource>();

            audioTrack.AudioSource.clip = audioTrack.AudioTrack;
            audioTrack.AudioSource.volume = audioTrack.AudioVolume;
            audioTrack.AudioSource.loop = audioTrack.AudioLoop;
        }
    }

    //This function will play the audio track, based on the name
    public void PlayAudioTrack(string audioName)
    {
        //Find the audio name in the audio list
        AudioList audioTrack = Array.Find(AudioTracks, track => track.AudioName == audioName);

        //Check if audio is not playing and play it
        if (!audioTrack.AudioSource.isPlaying)
        {
            audioTrack.AudioSource.Play();
        }
    }
    
    //This function will pause the audio track, based on the name
    public void PauseAudioTrack(string audioName)
    {
        AudioList audioTrack = Array.Find(AudioTracks, track => track.AudioName == audioName);

        //Check if audio is playing and pause it
        if (audioTrack.AudioSource.isPlaying)
        {
            audioTrack.AudioSource.Pause();
        }
    }

    //This function will pause audio track, based on the name
    public void StopAudioTrack(string audioName)
    {
        AudioList audioTrack = Array.Find(AudioTracks, track => track.AudioName == audioName);
        audioTrack.AudioSource.Stop();
    }
}
