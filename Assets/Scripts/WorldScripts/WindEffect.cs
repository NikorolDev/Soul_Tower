using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script controls the wind effect that will apply the force to the player
/// </summary>
public class WindEffect : MonoBehaviour
{
    //Private feilds
    private float m_windDelayTime; //Time of each blizzard phase
    private AreaEffector2D m_windEffector; //Reference to the area effector

    //Static int that sets the blizzard pahse
    public static int BlizzardPhase = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Grab reference to the area effector
        m_windEffector = GetComponent<AreaEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Set delta time to the wind time
        m_windDelayTime += Time.deltaTime;

        //Based on the blizzrd phase it will change force that will be applied to the player. This will be a constant timed loop
        switch (BlizzardPhase)
        {
            case 1: //Left
                if (m_windDelayTime >= 10)
                {
                    m_windEffector.forceMagnitude = 0;
                    BlizzardPhase = 3;
                }
                break;

            case 2: //Right
                if (m_windDelayTime >= 21)
                {
                    m_windEffector.forceMagnitude = 0;
                    BlizzardPhase = 3;
                }
                break;

            case 3: //Down
                if(m_windDelayTime >= 11 && m_windDelayTime < 21)
                {
                    m_windEffector.forceMagnitude = -20;
                    BlizzardPhase = 2;
                }
                else if(m_windDelayTime >= 22)
                {                
                    m_windEffector.forceMagnitude = 20;
                    BlizzardPhase = 1;
                    m_windDelayTime = 0;
                }
                break;
        }
    }
}
