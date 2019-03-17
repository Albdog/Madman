using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneManager : MonoBehaviour {

    public Text text;
    public static int leftWingFBCount, rightWingFBCount, middleBuildingFBcount, outsideFBcount;
    public static bool isLeftWingComplete, isRightWingComplete, isMiddleBuildingComplete, isOutsideComplete;

	// Use this for initialization
	void Start () {
        leftWingFBCount = 0;
        rightWingFBCount = 0;
        middleBuildingFBcount = 0;
        outsideFBcount = 0;

        isLeftWingComplete = false;
        isRightWingComplete = false;
        isMiddleBuildingComplete = false;
        isOutsideComplete = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(leftWingFBCount == 3) isLeftWingComplete = true;
        if(rightWingFBCount == 2) isRightWingComplete = true;
        if(middleBuildingFBcount == 2) isMiddleBuildingComplete = true;
        if(outsideFBcount == 2) isOutsideComplete = true;

        text.text =
            "Outside - " + outsideFBcount + "/2\n" +
            "Left Wing - " + leftWingFBCount + "/3\n" +
            "Middle Bldg - " + middleBuildingFBcount + "/2\n" +
            "Right Wing - " + rightWingFBCount + "/2\n";
    }
}
