﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackTrigger : MonoBehaviour {

    public int dmg = 5;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.isTrigger != true && col.CompareTag("Enemy"))
        {
            col.SendMessageUpwards("Damage", dmg);
        }

        if (col.CompareTag("Projectile"))
        {
            col.GetComponent<Rigidbody2D>().velocity *= -1;
        }
    }
}
