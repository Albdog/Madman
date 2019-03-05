using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneManager : MonoBehaviour {

    public Text text;

	// Use this for initialization
	void Start () {
        text.text =
            "Outside - 1/2\n" +
            "Bldg 1 - 0/3\n" +
            "Bldg 2 - 2/2\n" +
            "Bldg 4 - 1/2\n";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
