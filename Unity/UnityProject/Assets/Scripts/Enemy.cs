using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject player;

    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == player.name)
        {
            //Vector3 bulletPos = new Vector3(0, 0, 0);
            //Instantiate(bullet, bulletPos, transform.rotation);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == player.name)
        {
            print("omg overlapend let him go");
        }
    }

}
