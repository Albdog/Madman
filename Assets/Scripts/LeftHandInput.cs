using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LeftHandInput : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj = null;
    public SteamVR_Controller.Device device;

    public GameObject leftHand;
    public GameObject flashlight;
    public GameObject phone;

    private int modelNumber;

    private bool doorCollide, windowCollide, tableCollide;
    public static bool doorActivate, windowActivate, tableActivate;

    // Use this for initialization
    void Start() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        
        flashlight.SetActive(false);
        phone.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        device = SteamVR_Controller.Input((int) trackedObj.index);

        //trigger
        if(device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) {
            if(doorCollide) doorActivate = true;
            if(windowCollide) windowActivate = true;
            if(tableCollide) tableActivate = true;
        }

        if(device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) {
            if(doorActivate) doorActivate = false;
            if(windowActivate) windowActivate = false;
            if(tableActivate) tableActivate = false;
        }

        Vector2 triggerValue = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger);

        //grip
        if(device.GetPressDown(SteamVR_Controller.ButtonMask.Grip)) {
            //ModelSwitch();
        }

        if(device.GetPressUp(SteamVR_Controller.ButtonMask.Grip)) {
            print("grip up");
        }

        Vector2 touchValue = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);

        //touchpad
        if(device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
            float x = touchValue.x;
            float y = touchValue.y;

            float newX = (x - y) / Mathf.Sqrt(2);
            float newY = (x + y) / Mathf.Sqrt(2);

            if(newX >= 0) {
                if(newY >= 0) ModelSwitch(0); //right
                else print("other"); //down
            } else {
                if(newY >= 0) ModelSwitch(1); //up
                else ModelSwitch(2); //left
            }
        }

        if(device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) {
            //print("touchpad up");
        }

        
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Door")) doorCollide = true;
        if(other.CompareTag("Window")) windowCollide = true;
        if(other.CompareTag("TableAndChair")) tableCollide = true;
    }

    void OnTriggerExit(Collider other) {
        if(other.CompareTag("Door")) doorCollide = false;
        if(other.CompareTag("Window")) windowCollide = false;
        if(other.CompareTag("TableAndChair")) tableCollide = false;
    }

    private void ModelSwitch(int area) {
        if(area == 0) { //hand
            leftHand.SetActive(true);
            flashlight.SetActive(false);
            phone.SetActive(false);
        } else if(area == 1) { //flash light
            leftHand.SetActive(false);
            flashlight.SetActive(true);
            phone.SetActive(false);
        } else if(area == 2) { //phone
            leftHand.SetActive(false);
            flashlight.SetActive(false);
            phone.SetActive(true);
        }
    }
}
