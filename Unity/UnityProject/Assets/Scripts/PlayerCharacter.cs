using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private Rigidbody2D playerRB;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public int extraJumps;
    private int extraJumpsLeft;
    

    // Start is called before the first frame update
    void Start()
    {
        extraJumpsLeft = extraJumps;
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isGrounded == true)
        {
            extraJumpsLeft = extraJumps;
        }


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

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);



        float horizontal = Input.GetAxisRaw("Horizontal");

        playerRB.velocity = new Vector2(horizontal*speed, playerRB.velocity.y);

        if (facingRight == false && horizontal > 0)
        {
            flip();
        }
        else if (facingRight == true && horizontal < 0)
        {
            flip();
        }

    }

    private void flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }


}
