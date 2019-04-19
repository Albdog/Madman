using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RightHandInput : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj = null;
    public SteamVR_Controller.Device device;

    private bool doorCollide, windowCollide, tableCollide, mainSwitchCollide, fuseBoxCollide;
    public static bool doorActivate, windowActivate, tableActivate, mainSwitchActivate, fuseBoxActivate;

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
            if(tableCollide) tableActivate = true;
            if(mainSwitchCollide) mainSwitchActivate = true;
            if(fuseBoxCollide) fuseBoxActivate = true;
        }

        if(device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) {
            if(doorActivate) doorActivate = false;
            if(windowActivate) windowActivate = false;
            if(tableActivate) tableActivate = false;
            if(mainSwitchCollide) mainSwitchActivate = false;
            if(fuseBoxCollide) fuseBoxActivate = false;
        }

        Vector2 triggerValue = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger);

        if(device.GetPressDown(SteamVR_Controller.ButtonMask.Grip)) {
            
        }

        if(device.GetPressUp(SteamVR_Controller.ButtonMask.Grip)) {
            
        }

        //touchpad
        if(device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
            
        }

        if(device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) {
            
        }

        Vector2 touchValue = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Door")) doorCollide = true;
        if(other.CompareTag("Window")) windowCollide = true;
        if(other.CompareTag("TableAndChair")) tableCollide = true;
        if(other.CompareTag("Main Switch")) mainSwitchCollide = true;
        if(other.CompareTag("Outside Fuse Box") || other.CompareTag("Middle Building Fuse Box") || other.CompareTag("Left Wing Fuse Box") || other.CompareTag("Right Wing Fuse Box")) fuseBoxCollide = true;
    }

    void OnTriggerExit(Collider other) {
        if(other.CompareTag("Door")) doorCollide = false;
        if(other.CompareTag("Window")) windowCollide = false;
        if(other.CompareTag("TableAndChair")) tableCollide = false;
        if(other.CompareTag("Main Switch")) mainSwitchCollide = false;
        if(other.CompareTag("Outside Fuse Box") || other.CompareTag("Middle Building Fuse Box") || other.CompareTag("Left Wing Fuse Box") || other.CompareTag("Right Wing Fuse Box")) fuseBoxCollide = false;
    }
}
