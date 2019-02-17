using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject bullet;
    
    private Collider2D enemyAttackRange;

    public float fireRate;
    private float nextFire;

    public GameObject turretEnd;
    private Vector2 turretPosition;

    private bool playerInRange = false;

    public GameObject bulletParent;
    public int bulletTotalCount;
    public List<GameObject> bulletPool;
    private int currentBulletsShot = 0;



    void Awake()
    {
    
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
    }

    void Update()
    {
        CheckIfTimeToFire();
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire && playerInRange)
        {
            if (currentBulletsShot >= bulletPool.Count)
            {
                currentBulletsShot = 0;
            }
            
            if (bulletPool[currentBulletsShot].activeSelf == false)
            {
                bulletPool[currentBulletsShot].transform.position = turretPosition;
                bulletPool[currentBulletsShot].SetActive(true);
                nextFire = Time.time + fireRate;
                currentBulletsShot++;
            }
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
}
