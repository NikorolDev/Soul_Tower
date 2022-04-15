using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This script controls how the stopwatch is displayed
/// </summary>
public class Stopwatch : MonoBehaviour
{
    private TextMeshProUGUI m_textStopwatch;

    // Start is called before the first frame update
    void Start()
    {
        m_textStopwatch = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //Split the stopwatch into seconds, minutes and hours and make it a string
        string stopwatchSeconds = (PlayerData.Stopwatch % 60).ToString("00");
        string stopwatchMinutes = Mathf.Floor((PlayerData.Stopwatch % 3600) / 60).ToString("00");
        string stopwatchHours = Mathf.Floor((PlayerData.Stopwatch % 216000) / 3600).ToString("00");
        m_textStopwatch.text = "Time: " + stopwatchHours + ":" + stopwatchMinutes + ":" + stopwatchSeconds;

        if(MainMenu.InMainMenu == true)
        {
            stopwatchSeconds = (PlayerPrefs.GetFloat("Session Time") % 60).ToString("00");
            stopwatchMinutes = Mathf.Floor((PlayerPrefs.GetFloat("Session Time") % 3600) / 60).ToString("00");
            stopwatchHours = Mathf.Floor((PlayerPrefs.GetFloat("Session Time") % 216000) / 3600).ToString("00");
            m_textStopwatch.text = "Time: " + stopwatchHours + ":" + stopwatchMinutes + ":" + stopwatchSeconds;
        }

        if (OutroSequence.InOutroSequence == true)
        {
            stopwatchSeconds = (PlayerData.Stopwatch % 60).ToString("00");
            stopwatchMinutes = Mathf.Floor((PlayerData.Stopwatch % 3600) / 60).ToString("00");
            stopwatchHours = Mathf.Floor((PlayerData.Stopwatch % 216000) / 3600).ToString("00");
            m_textStopwatch.text = "Time: " + stopwatchHours + ":" + stopwatchMinutes + ":" + stopwatchSeconds;
        }


    }
}
