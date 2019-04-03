using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private GameObject playerShield;
    private GameObject player;
    private Rigidbody2D playerRB;
    private PlayerCharacter playerCharacter;
    private Vector2 respawnPoint;
    private Animator animator;

    public int playerHP;
    public bool isDead;


    private void Start()
    {
        playerShield = GameObject.FindGameObjectWithTag("Shield");
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
        playerCharacter = player.GetComponent<PlayerCharacter>();
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        animator = player.GetComponent<Animator>();
    }

    private void Update()
    {
        CheckDead();
    }

    private void CheckDead()
    {
        if (playerHP <= 0 && isDead != true)
        {
            isDead = true;
            animator.SetBool("IsDead", true);

            playerRB.simulated = false;

            if (playerShield.activeSelf == true)
            {
                playerShield.SetActive(false);
            }

            StartCoroutine(DeathTimer());

        }
    }

    IEnumerator DeathTimer()
    {
        print(animator.GetBool("IsDead"));

        yield return new WaitForSecondsRealtime(2);

        player.transform.position = respawnPoint;
        isDead = false;
        animator.SetBool("IsDead", false);
        playerHP = 1;
        playerRB.simulated = true;

        player.SetActive(true);
    }
}
