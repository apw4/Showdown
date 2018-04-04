using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class door : MonoBehaviour {

    public int LevelToLoad;

    private gameMaster gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            gm.inputText.text = ("[E]");
            if (Input.GetKeyDown("e"))
            {
                Application.LoadLevel(LevelToLoad);
            }
        }

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKeyDown("e"))
            {
                Application.LoadLevel(LevelToLoad);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            gm.inputText.text = ("");
        }
    }

}
