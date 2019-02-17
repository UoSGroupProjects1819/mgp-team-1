using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject player;
    private GameObject playerShield;

    //Bullet direction and speed manipulation
    private Rigidbody2D bulletRB;
    private Vector2 moveDirection;
    public float bulletMoveSpeed;

    private bool bulletInitialised = false;
    private bool transformRun = false;

    public float timer;
    private float privTimer;

    private CircleCollider2D bulletCircleCollider;


    void Start()
    {
        privTimer = timer;
        bulletRB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerShield = GameObject.FindGameObjectWithTag("Shield");
        bulletCircleCollider = GetComponent<CircleCollider2D>();
    }


    void Update()
    {
        bulletInitialised = true;

        if (bulletInitialised == true && transformRun == false)
        {
            transformRun = true;
            TransformBullet();
        }

        privTimer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            DeactivateBullet();
        }
    }

    //moveDirection = Towards players current position
    void TransformBullet()
    {
        moveDirection = (player.transform.position - transform.position).normalized * bulletMoveSpeed;
        bulletRB.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == player.name || collision.gameObject.tag == "Ground")
        {
            if (collision.gameObject.name == player.name)
            {
                //Hurt the player
                print("Ouchie I have been hurt");
            }

            DeactivateBullet();
        }

        //avoids null references when shield is set to inactive
        if (playerShield != null)
        {
            //Needs to be set as a trigger to not be stopped by other bullets
            if (collision.gameObject.tag == playerShield.tag)
            {
                bulletCircleCollider.isTrigger = true;
            }
        }
    }
    
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground" || other.tag == "Enemy" && bulletCircleCollider.isTrigger == true)
        {
            if (other.tag == "Enemy" && other.offset.x == 0)
            {
                print("An enemy has been slain");

                DeactivateBullet();
            }

            if (other.tag == "Ground")
            {
                DeactivateBullet();
            }
        }
    }

    //Reset all variables for the bullet so it can act as normal on next spawn
    private void DeactivateBullet()
    {
        bulletInitialised = false;
        transformRun = false;
        privTimer = timer;
        bulletCircleCollider.isTrigger = false;
        gameObject.SetActive(false);
    }
}
