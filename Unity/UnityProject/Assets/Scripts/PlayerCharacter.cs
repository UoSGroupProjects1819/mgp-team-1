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

    private GameObject playerShield;

    private Vector2 centerCircle;
    private Vector2 mousePos;

    public float innerCircleRadius;
    public float outerCircleRadius;

    void Start()
    {
        extraJumpsLeft = extraJumps;
        playerRB = GetComponent<Rigidbody2D>();
        playerShield = GameObject.FindGameObjectWithTag("Shield");
    }
    

    private void Update()
    {
        //If empty object on player is touching ground
        if (isGrounded == true)
        {
            extraJumpsLeft = extraJumps;
        }

        centerCircle = transform.position;
        
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 diff = mousePos - centerCircle;
        float distance = diff.magnitude;

        
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


        //TO DO: ROTATE THE SHIELD AS WELL AS MOVING IT. NOT SURE HOW TO DO THIS YET
        Vector2 distanceFromPlayer = playerShield.transform.position - transform.position;

        


        //Using velocity and forces to move to not mess up the physics systems
        if (Input.GetKeyDown(KeyCode.W) && extraJumpsLeft > 0)
        {
            playerRB.velocity = Vector2.up * jumpForce;
            extraJumpsLeft--;
        }
        else if (Input.GetKeyDown(KeyCode.W) && extraJumpsLeft == 0 && isGrounded == true)
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

        playerRB.velocity = new Vector2(horizontal*speed, playerRB.velocity.y);

        
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


}
