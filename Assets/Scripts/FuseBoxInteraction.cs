using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBoxInteraction : MonoBehaviour {

    private bool isFixed, playerEntered;
    private SoundEffectsManager sfx;

    // Use this for initialization
    void Start () {
        isFixed = false;
        sfx = new SoundEffectsManager();
    }
	
	// Update is called once per frame
	void Update () {
		if(MainSwitchInteraction.isDisabled) {
            if(playerEntered && (LeftHandInput.fuseBoxActivate || RightHandInput.fuseBoxActivate) && !isFixed) {
                switch(gameObject.tag) {
                case "Outside Fuse Box":
                    if(PhoneManager.outsideFBcount < 2) PhoneManager.outsideFBcount++;
                    else PhoneManager.outsideFBcount = 2;
                    break;
                case "Middle Building Fuse Box":
                    if(PhoneManager.middleBuildingFBcount < 2) PhoneManager.middleBuildingFBcount++;
                    else PhoneManager.middleBuildingFBcount = 2;
                    break;
                case "Left Wing Fuse Box":
                    if(PhoneManager.leftWingFBCount < 3) PhoneManager.leftWingFBCount++;
                    else PhoneManager.leftWingFBCount = 3;
                    break;
                case "Right Wing Fuse Box":
                    if(PhoneManager.rightWingFBCount < 2) PhoneManager.rightWingFBCount++;
                    else PhoneManager.rightWingFBCount = 2;
                    break;
                default:
                    break;
                }

                sfx.PlaySoundEffect("breaker fix");
                isFixed = true;
            }
        }
	}

    void OnTriggerEnter(Collider other) {
        playerEntered = true;
    }

    void OnTriggerExit(Collider other) {
        playerEntered = false;
    }
}
