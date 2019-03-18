using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D playerRB;
    private GameObject playerShield;
    private PlayerHealth playerHealth;

    public int spikeDamage = 1;
    public float ShieldBounceForce;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
        playerShield = GameObject.FindGameObjectWithTag("Shield");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == player.name)
        {
            //Damage player here
            DamagePlayer(spikeDamage);
        }

        //same code will go into the enemy or bullet script to allow player to bounce on top of bullets.
        //NOT TESTED AS DO NOT HAVE CONTROLLER
        if (collision.gameObject.name == playerShield.name /*&& playerShield.position is below player*/)
        {
            playerRB.velocity = Vector2.up * ShieldBounceForce;
        }
    }

    public void DamagePlayer(int m_spikeDamage)
    {
        playerHealth.playerHP -= m_spikeDamage;
        print("player health = " + playerHealth.playerHP);
    }
}
