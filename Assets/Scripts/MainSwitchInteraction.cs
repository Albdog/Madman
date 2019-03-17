using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSwitchInteraction : MonoBehaviour {

    public static bool isDisabled;

	// Use this for initialization
	void Start () {
        isDisabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if((LeftHandInput.mainSwitchActivate || RightHandInput.mainSwitchActivate) && (!isDisabled)) isDisabled = true;

        if((LeftHandInput.mainSwitchActivate || RightHandInput.mainSwitchActivate) && PhoneManager.isLeftWingComplete && PhoneManager.isMiddleBuildingComplete && PhoneManager.isRightWingComplete && PhoneManager.isOutsideComplete) isDisabled = false;
	}
}
