using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour
{

    //walking enemy
    private Rigidbody2D enemyRB;
    private int direction;

    public float rightDist;
    public float leftDist;
    public int movingSpeed;

    //player
    private PlayerHealth playerHealth;
    private GameObject player;
    public GameObject bullet;
    public GameObject bouncingbullet;

    // Use this for initialization
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerHealth>();
        direction = -1;
        leftDist = -leftDist + transform.position.x;
        rightDist = rightDist + transform.localPosition.x;

    }

    // Update is called once per frame
    void Update()
    {

        switch (direction)
        {
            case -1:
                // Moving Left
                if (transform.localPosition.x > leftDist)
                {
                    enemyRB.velocity = new Vector2(-movingSpeed, 0);
                }
                else
                {
                    direction = 1;
                    Flip();
                }
                break;
            case 1:
                //Moving Right
                if (transform.localPosition.x < rightDist)
                {
                    enemyRB.velocity = new Vector2(movingSpeed, 0);
                }
                else
                {
                    direction = -1;
                    Flip();
                }
                break;
        }
    }

    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DamagePlayer(1);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            print("Collision Detected");
            gameObject.SetActive(false);
        }
    }

    public void DamagePlayer(int m_bulletDamage)
    {
        playerHealth.playerHP -= m_bulletDamage;
    }

}
