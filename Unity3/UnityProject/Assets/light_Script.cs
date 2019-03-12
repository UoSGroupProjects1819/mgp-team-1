using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_Script : MonoBehaviour
{
    button_Collision buttonActive;
    public Sprite newSprite;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(buttonActive) {
            gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        }
    }
}
