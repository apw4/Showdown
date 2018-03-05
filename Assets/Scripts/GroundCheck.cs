﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    private PlayerController player;

	// Use this for initialization
	void Start () {
        player = gameObject.GetComponentInParent<PlayerController>();
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        player.grounded = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        player.grounded = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        player.grounded = false;
    }


    // Update is called once per frame
    void Update () {
		
	}
}
