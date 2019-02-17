using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletMoveSpeed;

    private Rigidbody2D bulletRB;

    private GameObject target;
    private Vector2 moveDirection;

    private bool initialised = false;
    private bool transformRun = false;

    public float timer;
    private float privTimer;

    private CircleCollider2D bulletCircleCollider;
    private GameObject shield;

    void Start()
    {

        privTimer = timer;
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        shield = GameObject.FindGameObjectWithTag("Shield");
        bulletCircleCollider = GetComponent<CircleCollider2D>();
    }


    void Update()
    {
        initialised = true;

        if (initialised == true && transformRun == false)
        {
            transformRun = true;
            TransformBullet();
        }

        privTimer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            initialised = false;
            transformRun = false;
            privTimer = timer;
            gameObject.SetActive(false);
        }
    }


    void TransformBullet()
    {
        moveDirection = (target.transform.position - transform.position).normalized * bulletMoveSpeed;
        bulletRB.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == target.name || other.gameObject.tag == "Ground")
        {
            if (other.gameObject.name == target.name)
            {
                //hurt the player
            }

            initialised = false;
            transformRun = false;
            privTimer = timer;
            gameObject.SetActive(false);
        }

        //needs to be set as a trigger to not be stopped by other bullets
        else if (other.gameObject.name == shield.name)
        {
            bulletCircleCollider.isTrigger = true;
        }
    }
    

    //This needs tidying up or re-doing with ray casts.
    //for now it just uses physics and bounces off the shield 
    //this means i have to set it to a trigger when its hit the shield so it doesnt get stopped by other bullets
    //coming towards the player
    //this also means i have to check that it overlaps with the correct enemy collider as they have 2
    //the offset of the correct collider is 0 but need a better way to do this
    //as the code is getting very messy

    //TODO: Make the shield follow the cursor but refined to the player.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground" || other.tag == "Enemy" && bulletCircleCollider.isTrigger == true)
        {
            if (other.tag == "Enemy" && other.offset.x == 0)
            {
                print("An enemy has been slain");

                initialised = false;
                transformRun = false;
                privTimer = timer;
                bulletCircleCollider.isTrigger = false;
                gameObject.SetActive(false);
            }

            if (other.tag == "Ground")
            {
                initialised = false;
                transformRun = false;
                privTimer = timer;
                bulletCircleCollider.isTrigger = false;
                gameObject.SetActive(false);
            }

            
        }
    }
}
