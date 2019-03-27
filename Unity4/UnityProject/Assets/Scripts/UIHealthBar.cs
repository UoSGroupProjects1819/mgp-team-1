using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Slider healthbar;
    void Update()
    {
        healthbar.value = healthBarValue();
    }

    public int healthBarValue() {
        return GameObject.Find("Player").GetComponent<PlayerHealth>().playerHP;
    }
}
