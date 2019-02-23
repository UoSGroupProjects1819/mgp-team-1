using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject bullet;
    
    public float fireRate;
    private float nextFire;

    public GameObject turretEnd;
    private Vector2 turretPosition;

    private Collider2D enemyAttackRange;
    private bool playerInRange = false;

    public GameObject bulletParent;
    public List<GameObject> bulletPool;
    public int bulletTotalCount;
    private int currentBulletsShot = 0;

    private PlayerHealth playerHealth;
    public int enemyHealth;

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
        enemyAttackRange = GetComponent<CircleCollider2D>();
        nextFire = Time.time;
        turretPosition = turretEnd.transform.position;
        playerHealth = player.GetComponent<PlayerHealth>();
    }


    void Update()
    {
        CheckIfTimeToFire();

        if (enemyHealth <= 0)
        {
            gameObject.SetActive(false);
        }
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

    public void DamagePlayer(int bulletDamage)
    {
        playerHealth.playerHP -= bulletDamage;
        print("player health = " + playerHealth.playerHP);
    }

    public void DamageSelf(int bulletDamage)
    {
        enemyHealth -= bulletDamage;
        print("enemy health = " + enemyHealth);
    }
}
