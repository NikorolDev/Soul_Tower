using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script controls how the blizzard snow particles move on the screen
/// </summary>
public class SnowBlowing : MonoBehaviour
{
    //Private reference to the particle system to get the particle system velocity and speed modifier
    private ParticleSystem m_snowBlizzardParticles;

    //This float property will set the transition time and it will act as a multiplier. This will be set in the Inspector
    public float TransitionTime;

    // Start is called before the first frame update
    void Start()
    {
        //Grab reference to the particle system
        m_snowBlizzardParticles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //Set a variable for easier access to velocity over life time of the particle system
        var blizzardVelocity = m_snowBlizzardParticles.velocityOverLifetime;

        //Based on the blizzard phase it will change the direction
        switch (WindEffect.BlizzardPhase)
        {
            case 1: //Right
                //Using the lerp will give a smooth transition
                blizzardVelocity.x = Mathf.Lerp(blizzardVelocity.x.constant, 5, Time.deltaTime * TransitionTime);
                blizzardVelocity.z = Mathf.Lerp(blizzardVelocity.z.constant, 1.1f, Time.deltaTime * TransitionTime);
                blizzardVelocity.speedModifier = 2;
                break;

            case 2: //Left
                blizzardVelocity.x = Mathf.Lerp(blizzardVelocity.x.constant, -5, Time.deltaTime * TransitionTime);
                blizzardVelocity.z = Mathf.Lerp(blizzardVelocity.z.constant, 1.1f, Time.deltaTime * TransitionTime);
                blizzardVelocity.speedModifier = 2;
                break;

            case 3: //Down
                blizzardVelocity.x = Mathf.Lerp(blizzardVelocity.x.constant, 0, Time.deltaTime * TransitionTime);
                blizzardVelocity.z = Mathf.Lerp(blizzardVelocity.z.constant, 1.6f, Time.deltaTime * TransitionTime);
                blizzardVelocity.speedModifier = 1;
                break;
        }
    }
}
