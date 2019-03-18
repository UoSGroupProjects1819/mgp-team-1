using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == player.name)
        {
            //transition the level to another scene here.
            //
            //Option 1: Change scenes for each level.
            //
            //Option 2: Activate new level and deactivate old level, teleport player to entrance.
            //          Change scenes on new floor.
            //
            //Option 3: Have a door that opens when they stand on a button or get close to the door?
            //          this could also activate the enemies in the next room or similar.
            //          Change scenes on new floor.
        }
    }

}
