using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is for how the player will control the player.
/// </summary>
public class PlayerController : MonoBehaviour
{
    //Private feilds
    //Boolean feilds
    private bool m_isJumping; //To check if player has jumped
    private bool m_isGrounded; //To check if player is on the ground
    private bool m_isCharging; //To check if the player is charging his jump

    //These bools are to check if the audio has been played, to stop the audio looping when collided with platforms
    private bool m_hasLandingAudioPlayed;
    private bool m_hasHeadAudioPlayed;
    private bool m_hasLeftBodyAudioPlayed;
    private bool m_hasRightBodyAudioPlayed;

    //Integer feild to set direction to where the player will jump.
    private int m_pickedDirection;

    //private references
    private Animator m_playerAnimator; //Reference to the animater in Unity to change animation states
    private AudioManager m_audioManager; //Reference to the audio manager to use its functions
    private CapsuleCollider2D m_playerCapsuleCollider; //Reference to the capsule collider in order to change the size of it
    
    
    //Raycast feilds are used to check if they've collided with platforms to play audio
    private RaycastHit2D m_colliderHead;
    private RaycastHit2D m_colliderBodyLeft;
    private RaycastHit2D m_colliderBodyRight;
    private RaycastHit2D m_colliderFeet;
    
    //Static feilds
    public static bool ControlsEnabled; //this is to check if the controls are enabled
    public static float MoveInput; //This is float will reference the axis of where the player will move. This will be used in the screencaster script for detecting left and right movement

    //Player properties.
    //Float properties
    public float PlayerSpeed; //Speed of the player movement
    public float PlayerGroundOverlapRadius; //The area in which the collider falls into 
    public float PlayerJumpForce; //The force of the player's jump
    public float PlayerChargedForce; //The charge time that will impact the jump height
    
    //These 3 trasform properties grabs the position of the gizmo to check if player is on the ground
    public Transform PlayerFeetPosition; 
    public Transform PlayerLeftFootPosition; 
    public Transform PlayerRightFootPosition; 
    
    //Unity properties
    [HideInInspector]
    public Rigidbody2D m_playerRigidBody; //Reference to the rigid body of our player sprite, this is public so that we can use in other scripts, such as Camera Switcher
    public LayerMask GroundType; //The layer in the scene, which is used to check if the player is grounded

    //Start is called before the first frame update
    void Start()
    {
        //Get a reference to the components of the player sprite's animator, audio manager script, capsule collider and rigid body
        m_playerAnimator = GetComponent<Animator>();
        m_audioManager = FindObjectOfType<AudioManager>();
        m_playerCapsuleCollider = GetComponent<CapsuleCollider2D>();
        m_playerRigidBody = GetComponent<Rigidbody2D>();
    }

    //Fixed Update is called every fixed frame
    void FixedUpdate()
    {
        MoveInput = Input.GetAxisRaw("Horizontal"); //Gets mapped controls on the x axis. We are using raw, so that it is static, instead of smooth transition. This will link to controllers as well

        //Check if the player is crouching and grounded and if controls are enabled to enable movement
        if (m_isCharging == false && m_isGrounded == true && ControlsEnabled == true)
        {
            //Allow the player to move and Run idle or run aniamtion, as player is not airborne
            PlayerMovementControls();
            m_playerAnimator.SetBool("isAirborne", false);
        }
    }

    //Update is called once per frame
    void Update()
    {
        //Check if controls are enabled to also enable jumping
        if(ControlsEnabled == true)
        {
            PlayerJump();
            Cursor.visible = false; //Hide the mouse cursor

            //Check if the collider is not null and if the landing has been played and if the player is grounded
            if (m_colliderFeet.collider != null && m_hasLandingAudioPlayed == false && m_isGrounded == true)
            {
                //Set the audio played to true in order to stop audio playing and play the audio
                m_hasLandingAudioPlayed = true;
                m_audioManager.PlayAudioTrack("Landing Sound");
            }
        }
        else //unhide the mouse cursor when the controls are disabled
        {
            Cursor.visible = true;
        }

        //Set the raycasters
        m_colliderHead = Physics2D.Raycast(transform.position, Vector2.up, 0.43f, GroundType);
        m_colliderBodyLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.19f, GroundType);
        m_colliderBodyRight = Physics2D.Raycast(transform.position, Vector2.right, 0.2f, GroundType);
        m_colliderFeet = Physics2D.Raycast(transform.position, Vector2.down, 0.41f, GroundType);
    }

    //This function controls the movement of player when going left and right
    private void PlayerMovementControls()
    {        
        //if the player is moving right
        if (MoveInput > 0)
        {
            //set the default angle of the sprite and set the running animation to true
            transform.eulerAngles = new Vector3(0, 0, 0);
            m_playerRigidBody.velocity = new Vector2(MoveInput * PlayerSpeed, m_playerRigidBody.velocity.y);
            m_playerAnimator.SetBool("isRunning", true);

        }
        //if the player is moving left
        else if (MoveInput < 0)
        {
            //flip the angle of the sprite to face it towards the left side and set the running animation to true
            transform.eulerAngles = new Vector3(0, 180, 0);
            m_playerRigidBody.velocity = new Vector2(MoveInput * PlayerSpeed, m_playerRigidBody.velocity.y);
            m_playerAnimator.SetBool("isRunning", true);

        }
        //else set running animation to false, to run the idle animation
        else
        {
            m_playerAnimator.SetBool("isRunning", false);
        }
    }

    //This function controls the jumping of the player
    private void PlayerJump()
    {
        //If either feet positions are on the ground, which is determined based on the ground using the "Layer Mask" and the circle it overlaps with, then the player is grounded
        if(Physics2D.OverlapCircle(PlayerFeetPosition.position, PlayerGroundOverlapRadius, GroundType) ||
            Physics2D.OverlapCircle(PlayerLeftFootPosition.position, PlayerGroundOverlapRadius, GroundType) ||
            Physics2D.OverlapCircle(PlayerRightFootPosition.position, PlayerGroundOverlapRadius, GroundType))
        {
            m_isGrounded = true;
        }
        else
        {
            m_isGrounded = false;
        }

        //if the player is on the ground and mapped Jump buttons are pressed (Spacebar /A on the Xbox) is held down
        if (m_isGrounded == true && Input.GetButton("Jump"))
        {
            m_isCharging = true; //Set crouching to true and play the animation for it
            m_playerCapsuleCollider.size = new Vector2(m_playerCapsuleCollider.size.x, 0.81f); //Set Capsule size, this will avoid the sprite shifting upwards
            
            //Set charging animation to true and running to false
            m_playerAnimator.SetBool("isCharging", true); 
            m_playerAnimator.SetBool("isRunning", false);

            //if the player has chosen a direction, which left, right or go up.
            if (MoveInput > 0)
            {
                //Jump right
                m_pickedDirection = 1; //Set this value to the switcher that will add velocity to make the jump
                transform.eulerAngles = new Vector3(0, 0, 0); //Set the default angle of the sprite to make it face towards the right
            }
            else if (MoveInput < 0)
            {
                //Jump left
                m_pickedDirection = 2;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else //Jump upwards
            {
                m_pickedDirection = 3;
            }

            PlayerChargedForce += Time.deltaTime; //Increment the charge using delta time

            //if the jump charge is greate than 1, then set it to 1, as the player's max 
            if (PlayerChargedForce >= 1)
            {
                PlayerChargedForce = 1;

                //Switch from charging animation to fully charged animation
                m_playerAnimator.SetBool("isCharging", false);
                m_playerAnimator.SetBool("isFullyCharged", true);
            }
        }

        //if the player releases the jump button
        if (Input.GetButtonUp("Jump"))
        {            
            //This is to check if player has charged too little, so it will cancel the jump.
            if (m_isGrounded == true && PlayerChargedForce < 0.2)
            {
                m_isCharging = false;
            }

            m_isJumping = true; //set is jumping to true, to show that the player has jumped
            m_audioManager.PlayAudioTrack("Jump Sound"); //Play the jump sound

            //Turn off all jump charge animations and run airborne animation
            m_playerAnimator.SetBool("isCharging", false);
            m_playerAnimator.SetBool("isFullyCharged", false);
            m_playerAnimator.SetBool("isAirborne", true);        }

        //if the player has jumped
        if (m_isJumping == true)
        {
            m_playerCapsuleCollider.size = new Vector2(m_playerCapsuleCollider.size.x, 0.82f); //Reset the capsule size

            //Based on the direction the player set, add force to the player and make it jump in a parabola curve
            switch (m_pickedDirection)
            {
                case 1: //Right
                    //Distance, Height
                    m_playerRigidBody.AddForce(new Vector2(Mathf.Pow(PlayerChargedForce, 2)* PlayerJumpForce, (PlayerChargedForce* PlayerJumpForce)*2.3f), ForceMode2D.Force);            
                    break;

                case 2: //Left
                    m_playerRigidBody.AddForce(new Vector2(-Mathf.Pow(PlayerChargedForce, 2) * PlayerJumpForce, (PlayerChargedForce * PlayerJumpForce)*2.3f), ForceMode2D.Force);
                    break;

                case 3: //Up
                    m_playerRigidBody.AddForce(new Vector2(0, (PlayerChargedForce * PlayerJumpForce) * 2.3f));
                    break;
            }

            //Set jumping to false as the player is not jumping anymore and charge back to 0
            m_isJumping = false;
            PlayerChargedForce = 0;
        }

        //if the player is no longer on the ground
        if (m_isGrounded == false)
        {
            //Reset the values
            m_isCharging = false;
            m_hasLandingAudioPlayed = false;

            //Reset the collsion audio to be played and delay the colliding sounds playing to avoid any unnecessary audio being played at beginning of the player jump
            ResetCollisionAllAudio();
            Invoke("PlayCollisionAudio", 0.1f);
        }
        else //else, set it the colliding sounds played to false, to be played again
        {
            ResetCollisionAllAudio();
        }
    }

    //This function will play colliding sound based on raycaster
    private void PlayCollisionAudio()
    {
        //Hit on the head
        if (m_colliderHead.collider != null && m_hasHeadAudioPlayed == false)
        {
            m_hasHeadAudioPlayed = true;
            FindObjectOfType<AudioManager>().PlayAudioTrack("Colliding Sound");
        }

        //Hit on left side
        if (m_colliderBodyLeft.collider != null && m_hasLeftBodyAudioPlayed == false)
        {
            m_hasLeftBodyAudioPlayed = true;
            FindObjectOfType<AudioManager>().PlayAudioTrack("Colliding Sound");
            
        }

        //hit on the right side
        if (m_colliderBodyRight.collider != null && m_hasRightBodyAudioPlayed == false)
        {
            m_hasRightBodyAudioPlayed = true;
            FindObjectOfType<AudioManager>().PlayAudioTrack("Colliding Sound");
        }

        //This function is played to enable the colliding sound be played mre than once when hit by multiple platforms.
        ResetCollisionAllAudio();
    }

    //This function will reset collision played bools to be played again.
    private void ResetCollisionAllAudio()
    {
        m_hasHeadAudioPlayed = false;
        m_hasLeftBodyAudioPlayed = false;
        m_hasRightBodyAudioPlayed = false;
    }
}