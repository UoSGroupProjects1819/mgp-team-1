using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerHP;
    public bool isDead;

    private GameObject playerShield;

    private PlayerCharacter playerCharacter;

    private void Start()
    {
        playerShield = GameObject.FindGameObjectWithTag("Shield");
        playerCharacter = GetComponent<PlayerCharacter>();
    }

    private void Update()
    {
        CheckDead();
    }

    private void CheckDead()
    {
        if (playerHP <= 0)
        {
            isDead = true;
            if (playerShield.activeSelf == true)
            {
                playerShield.SetActive(false);
            }
            gameObject.SetActive(false);
        }
    }
}
