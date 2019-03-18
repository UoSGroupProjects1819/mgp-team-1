using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //player
    private GameObject player;
    private PlayerHealth playerHealth;

    //enemy
    private Vector2 turretPosition;
    private Collider2D enemyAttackRange;
    private bool playerInRange = false;
    private float nextFire;
    private int currentBulletsShot = 0;

    public GameObject turretEnd;
    public float fireRate;
    public int bulletTotalCount;

    //bullet
    public GameObject bullet;
    public GameObject bulletParent;
    public List<GameObject> bulletPool;


    void Awake()
    {
        //Instantiates all bullet pools for each enemy (Might be a way to do this more effeciently from one pool)
        for (int i = 0; i < bulletTotalCount; i++)
        {
            bulletPool.Add(Instantiate(bullet));
            bulletPool[i].SetActive(false);
            bulletPool[i].transform.parent = bulletParent.transform;
        }
    }


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyAttackRange = GetComponent<CircleCollider2D>();
        turretPosition = turretEnd.transform.position;
        nextFire = Time.time;
    }


    void Update()
    {
        CheckIfTimeToFire();
    }


    void CheckIfTimeToFire()
    {
        //Resets back to start of bullet pool
        if (currentBulletsShot >= bulletPool.Count)
        {
            currentBulletsShot = 0;
        }

        if (Time.time > nextFire && playerInRange && !playerHealth.isDead)
        {
            if (bulletPool[currentBulletsShot].activeSelf == false)
            {
                bulletPool[currentBulletsShot].transform.position = turretPosition;
                bulletPool[currentBulletsShot].SetActive(true);
                nextFire = Time.time + fireRate;

                currentBulletsShot++;
            }

            //Fail safe to make sure it keeps looping through the bullets even if one is still active
            //This will cause a slight time gap between a shot
            else
            {
                currentBulletsShot++;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == player.name)
        {
            playerInRange = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == player.name)
        {
            playerInRange = false;
        }
    }


    public void DamagePlayer(int m_bulletDamage)
    {
        playerHealth.playerHP -= m_bulletDamage;
    }



    //No longer having enemies hurt themselves with their bullets

    //public void DamageSelf(int bulletDamage)
    //{
    //    enemyHealth -= bulletDamage;
    //    print("enemy health = " + enemyHealth);
    //}
}

