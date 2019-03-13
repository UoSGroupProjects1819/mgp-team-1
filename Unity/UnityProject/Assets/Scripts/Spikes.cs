using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private GameObject player;
    private GameObject playerShield;
    public int spikeDamage = 1;
    private PlayerHealth playerHealth;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
    }

    public void DamagePlayer(int spikeDamage)
    {
        playerHealth.playerHP -= spikeDamage;
        print("player health = " + playerHealth.playerHP);
    }
}
