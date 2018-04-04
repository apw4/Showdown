using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameMaster : MonoBehaviour {

    public int cash;
    public Text cashText;

    public Text inputText;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        cashText.text = ("" + cash);

		
	}
}
