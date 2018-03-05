using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour {

    private bool attacking = false;
    private bool sheathed = true;
    private int slashammo = 1;

    public Collider2D attackTrigger;

    private Animator anim;

     void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        attackTrigger.enabled = false;
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.J) && !attacking)
        {
            attacking = true;
            
            attackTrigger.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            attacking = false;
            attackTrigger.enabled = false;
            sheathed = true;
            slashammo = 1;
        }

        if (attacking)
        {
            if (slashammo > 0)
            {
                slashammo -= 1;
            }
            else
            {
                attacking = false;
                attackTrigger.enabled = false;
                sheathed = false;
            }
        }
        anim.SetBool("Attacking", attacking);
        anim.SetBool("Sheathed", sheathed);
    }
}
