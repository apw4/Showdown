using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.isTrigger != true)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<PlayerController>().Damage(1);
            }

            if (col.CompareTag("Enemy"))
            {
                col.GetComponent<ThiefAI>().Damage(2);
            }

            Destroy(gameObject);
        }
    }
}
