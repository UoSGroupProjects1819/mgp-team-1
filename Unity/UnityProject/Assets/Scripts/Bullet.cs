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


    void Start()
    {
        privTimer = timer;
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
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


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == target.name || other.tag == "Ground")
        {
            if (other.name == target.name)
            {
                //hurt the player
            }

            initialised = false;
            transformRun = false;
            privTimer = timer;
            gameObject.SetActive(false);
        }
    }
}
