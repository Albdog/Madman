using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneManager : MonoBehaviour {

    public Text text;
    public static int leftWingFBCount, rightWingFBCount, middleBuildingFBcount, outsideFBcount;

	// Use this for initialization
	void Start () {
        leftWingFBCount = 0;
        rightWingFBCount = 0;
        middleBuildingFBcount = 0;
        outsideFBcount = 0;

        GameState.areFuseBoxesFixed = false;
    }
	
	// Update is called once per frame
	void Update () {


        if(!MainSwitchInteraction.isDisabled) {
            text.text =
            "Outside - " + outsideFBcount + "/2\n" +
            "Left Wing - " + leftWingFBCount + "/3\n" +
            "Middle Bldg - " + middleBuildingFBcount + "/2\n" +
            "Right Wing - " + rightWingFBCount + "/2\n";
        }
        else {
            text.text = "Disable main switch in 1st floor maintenance room.";
        }

        if((leftWingFBCount == 3) && (rightWingFBCount == 2) && (middleBuildingFBcount == 2) && (outsideFBcount == 2)) {
            GameState.areFuseBoxesFixed = true;
            text.text = "Go back and turn on main switch.";
        }
    }
}
