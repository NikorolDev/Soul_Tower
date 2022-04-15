using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 /// <summary>
 /// This script is to change camera positions when the player goes off the screen
 /// </summary>
public class CameraSwitcher : MonoBehaviour
{
    //Private referennce to the player controller to mainly get the rigidbody2D componenet
    private PlayerController player;

    //Game object reference to the camera
    private GameObject m_camera;

    // Start is called before the first frame update
    void Start()
    {
        //Get the reference to the player movement script and camera
        player = FindObjectOfType<PlayerController>();
        m_camera = GameObject.Find("Main Camera");
    }

    //This function will only get called once the player becomes invisible
    private void OnBecameInvisible()
    {
        //if the player is gpoing up
        if(player.m_playerRigidBody.velocity.y > 0 && m_camera != null)
        {
            m_camera.transform.position = new Vector3(0, m_camera.transform.position.y + 8.2f, -10); //Move the camera upwards
        }

        //if the player is going down
        if(player.m_playerRigidBody.velocity.y < 0 && m_camera != null)
        {
            m_camera.transform.position = new Vector3(0, m_camera.transform.position.y - 8.2f, -10); //Move the camera down
        }
    }
}
