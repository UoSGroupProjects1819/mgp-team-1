using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Player Shield
    private GameObject player;
    private GameObject playerShield;

    //Enemy
    private Enemy enemyScript;

    //Bullet
    private Rigidbody2D bulletRB;
    private CircleCollider2D bulletCircleCollider;
    private Vector2 moveDirection;
    private bool bulletInitialised = false;
    private bool transformRun = false;
    private float privTimer;
    private int extraBouncesPriv;

    public float bulletMoveSpeed;
    public float timer;
    public int bulletDamage;
    public int extraBounces;


    void Start()
    {
        privTimer = timer;
        bulletRB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerShield = GameObject.FindGameObjectWithTag("Shield");
        bulletCircleCollider = GetComponent<CircleCollider2D>();

        enemyScript = GetComponentInParent<Enemy>();

        extraBouncesPriv = extraBounces;
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
                //Damage player here
                enemyScript.DamagePlayer(bulletDamage);
            }

            if (extraBouncesPriv == 0)
            {
                DeactivateBullet();
            }

            else
                extraBouncesPriv--;
        }
    }


    //Reset all variables for the bullet so it can act as normal on next spawn
    private void DeactivateBullet()
    {
        bulletInitialised = false;
        transformRun = false;
        privTimer = timer;
        bulletCircleCollider.isTrigger = false;
        extraBouncesPriv = extraBounces;
        gameObject.SetActive(false);
    }



    //only needed if we decide to kill enemies (might have to change all bullet code especially for bouncing bullets if needed)

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "Ground" || other.tag == "Enemy" && bulletCircleCollider.isTrigger == true)
    //    {
    //        if (other.tag == "Enemy" && other.offset.x == 0)
    //        {
    //            enemyScript.DamageSelf(bulletDamage);

    //            DeactivateBullet();
    //        }

    //        if (other.tag == "Ground" && extraBouncesPriv == 0)
    //        {
    //            DeactivateBullet();
    //        }

    //    }
    //}


    //only needed if we decide to kill enemies (this goes into onCollisionEnter)

    ////avoids null references when shield is set to inactive
    //if (playerShield != null)
    //{
    //    //Needs to be set as a trigger to not be stopped by other bullets
    //    if (collision.gameObject.tag == playerShield.tag)
    //    {
    //        bulletCircleCollider.isTrigger = true;
    //    }
    //}


}
