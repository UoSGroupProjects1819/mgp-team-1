﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private GameObject playerShield;
    private GameObject player;
    private PlayerCharacter playerCharacter;
    private Vector2 respawnPoint;

    public int playerHP;
    public bool isDead;


    private void Start()
    {
        playerShield = GameObject.FindGameObjectWithTag("Shield");
        player = GameObject.FindGameObjectWithTag("Player");
        playerCharacter = player.GetComponent<PlayerCharacter>();
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform.position;
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
            if (playerShield.activeSelf == true)
            {
                playerShield.SetActive(false);
            }

            StartCoroutine(DeathTimer());

        }
    }

    IEnumerator DeathTimer()
    {
        player.SetActive(false);

        yield return new WaitForSecondsRealtime(2);

        player.transform.position = respawnPoint;
        isDead = false;
        playerHP = 1;

        player.SetActive(true);
    }
}