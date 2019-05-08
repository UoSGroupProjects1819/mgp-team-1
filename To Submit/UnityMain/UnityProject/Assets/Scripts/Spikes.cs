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
        playerHealth = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerHealth>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //same code will go into the enemy or bullet script to allow player to bounce on top of bullets.
        //NOT TESTED AS DO NOT HAVE CONTROLLER
        if (collision.gameObject.name == playerShield.name /*&& playerShield.position is below player?*/)
        {
            if (playerRB.velocity.y < 0)
            {
                playerRB.velocity = new Vector2(0, ShieldBounceForce);
            }

        }

        if (collision.gameObject.name == player.name)
        {
            //Damage player here
            DamagePlayer(spikeDamage);
        }
    }


    public void DamagePlayer(int m_spikeDamage)
    {
        playerHealth.playerHP -= m_spikeDamage;
    }
}
