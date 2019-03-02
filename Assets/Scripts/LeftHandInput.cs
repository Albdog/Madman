using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LeftHandInput : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj = null;
    public SteamVR_Controller.Device device;

    public GameObject leftHand;
    public GameObject flashlight;

    private int modelNumber;

    private bool doorCollide, windowCollide;
    public static bool doorActivate, windowActivate;

    // Use this for initialization
    void Start() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

        modelNumber = 0;
        flashlight.SetActive(false);
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
            ModelSwitch();
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

    private void ModelSwitch() {
        if(modelNumber == 0) {
            leftHand.SetActive(false);
            flashlight.SetActive(true);
            modelNumber = 1;
        }
        else if(modelNumber == 1) {
            leftHand.SetActive(true);
            flashlight.SetActive(false);
            modelNumber = 0;
        }
    }
}
