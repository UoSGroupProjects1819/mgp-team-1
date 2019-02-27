using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    //Movement Variables
    public float speed;
    public float jumpForce;
    
    private Rigidbody2D playerRB;

    private bool facingRight = true;

    //Variables for jumping
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public int extraJumps;
    private int extraJumpsLeft;

    //variables for player shield
    private GameObject playerShield;

    private Vector2 centerCircle;
    private Vector2 joystickPos;
   
    public float circleRadius;
    //private Vector2 mousePos;
    public float shieldRotationSpeed;

    private PlayerHealth playerHealth;


    void Start()
    {
        extraJumpsLeft = extraJumps;
        playerRB = GetComponent<Rigidbody2D>();
        playerShield = GameObject.FindGameObjectWithTag("Shield");
        playerHealth = GetComponent<PlayerHealth>();
    }
    

    private void Update()
    {

        //If empty object on player is touching ground
        if (isGrounded == true)
        {
            extraJumpsLeft = extraJumps;
        }

        centerCircle = transform.position;
        joystickPos = new Vector2((Input.GetAxis("Horizontal2") + 0.01f), Input.GetAxis("Vertical2") + 0f);
        float distance = joystickPos.magnitude;
        
        //Default position is (0.01, 0) as I always add 0.01f to avoid NaN errors
        if (joystickPos == new Vector2(0.01f, 0f) || playerHealth.isDead)
        {
            playerShield.SetActive(false);
        }
        else
        {
            playerShield.SetActive(true);
        }

        //Makes the shield stay on a set line around the player so it doesn't collide with the player
        if (distance <= circleRadius || distance >= circleRadius)
        {
            playerShield.transform.position = centerCircle + (joystickPos / distance) * circleRadius;
        }

        //Rotates the shield of the player on right and left trigger
        if (playerShield.activeSelf && Input.GetAxisRaw("Fire1") >= 0.5)
            playerShield.transform.Rotate(0, 0, -shieldRotationSpeed);

        else if (playerShield.activeSelf && Input.GetAxisRaw("Fire1") <= -0.5)
            playerShield.transform.Rotate(0, 0, shieldRotationSpeed);;


        //Using velocity and forces to move to not mess up the physics system
        if (Input.GetButtonDown("Jump") && extraJumpsLeft > 0 && !playerHealth.isDead)
        {
            playerRB.velocity = Vector2.up * jumpForce;
            extraJumpsLeft--;
        }
        else if (Input.GetKeyDown(KeyCode.W) && extraJumpsLeft == 0 && isGrounded == true && !playerHealth.isDead)
        {
            playerRB.velocity = Vector2.up * jumpForce;
        }

    }


    private void FixedUpdate()
    {
        //"whatIsGround" is anything on the ground layer in the scene
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        //using raw for more accurate movement and no 'sliding'
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (!playerHealth.isDead)
        {
            playerRB.velocity = new Vector2(horizontal * speed, playerRB.velocity.y);
        }
        
        if (facingRight == false && horizontal > 0)
        {
            FlipSprite();
        }
        else if (facingRight == true && horizontal < 0)
        {
            FlipSprite();
        }       

    }


    private void FlipSprite()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }



    /* 
         * This is for use only with mouse to control the shield of the player
         * may implement a setting later to disable or enable mouse control. 
         * game is controller only for now.
         * 
         */


    /*
     * 
     * 
        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Vector2 diff = mousePos - centerCircle;
        //float distance = diff.magnitude;


        //Keeps the player within a doughnut styled shape based on inner and outer circle radius
        //current both set to 1 to keep it on a set line around the player
        if (distance <= innerCircleRadius || distance >= outerCircleRadius)
        {
            if (distance <= innerCircleRadius)
                playerShield.transform.position = centerCircle + (diff / distance) * innerCircleRadius;

            if (distance >= outerCircleRadius)
                playerShield.transform.position = centerCircle + (diff / distance) * outerCircleRadius;
        }
        else
        {
            playerShield.transform.position = mousePos;
        }
    *
    *
    */


}
