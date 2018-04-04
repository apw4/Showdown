using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCone : MonoBehaviour {
    public AI_Turret turretAI;
    public bool isleft = false;

	void Awake () {
        turretAI = gameObject.GetComponentInParent<AI_Turret>();
	}
	

	void OnTriggerStay2D(Collider2D col) {

        if (col.CompareTag("Player"))
        {
            if (isleft)
            {
                turretAI.Attack(false);
            }
            else
            {
                turretAI.Attack(true);
            }
        }
	}
}
