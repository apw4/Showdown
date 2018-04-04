using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefAttackCone : MonoBehaviour
{
    public ThiefAI ThiefAI;

    void Awake()
    {
        ThiefAI = gameObject.GetComponentInParent<ThiefAI>();
    }


    void OnTriggerStay2D(Collider2D col)
    {

        if (col.CompareTag("Player"))
        {
            ThiefAI.Attack();

        }
    }
}
