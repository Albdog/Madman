using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveInput : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj = null;
    public SteamVR_Controller.Device device;

    private bool doorCollide, windowCollide;
    public static bool doorActivate, windowActivate;

	// Use this for initialization
	void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

    // Update is called once per frame
    void Update() {
        device = SteamVR_Controller.Input((int) trackedObj.index);

        //trigger
        if(device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) {
            if(doorCollide) doorActivate = true;
            if(windowCollide) windowActivate = true;
        }

        if(device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) {
            if(doorActivate) doorActivate = false;
            if(windowActivate) windowActivate = false;
        }

        Vector2 triggerValue = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger);

        //grip
        if(device.GetPressDown(SteamVR_Controller.ButtonMask.Grip)) {
            print("grip down");
        }

        if(device.GetPressUp(SteamVR_Controller.ButtonMask.Grip)) {
            print("grip up");
        }

        //touchpad
        if(device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
            print("touchpad down");
        }

        if(device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) {
            print("touchpad up");
        }

        Vector2 touchValue = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Door")) doorCollide = true;
        if(other.CompareTag("Window")) windowCollide = true;
    }

    void OnTriggerExit(Collider other) {
        if(other.CompareTag("Door")) doorCollide = false;
        if(other.CompareTag("Window")) windowCollide = false;
    }
}
