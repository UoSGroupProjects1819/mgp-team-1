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
    

    void Start()
    {
        extraJumpsLeft = extraJumps;
        playerRB = GetComponent<Rigidbody2D>();
    }
    

    private void Update()
    {
        //If empty object on player is touching ground
        if (isGrounded == true)
        {
            extraJumpsLeft = extraJumps;
        }

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
