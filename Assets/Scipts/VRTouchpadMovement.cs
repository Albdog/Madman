using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRTouchpadMovement : MonoBehaviour {

    public Transform rig;

    private EVRButtonId touchpad = EVRButtonId.k_EButton_SteamVR_Touchpad;
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device controller {  get { return SteamVR_Controller.Input((int) trackedObj.index);  } }
    private Vector2 axis = Vector2.zero;

	void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	void Update () {
		if(controller == null) {
            print("No controller found");
            return;
        }

        var device = SteamVR_Controller.Input((int) trackedObj.index);

        if(controller.GetTouch(touchpad)) {
            axis = device.GetAxis(.EVRButtonId.k_EButton_Axis0);

            if(rig != null) {
                rig.position += (transform.right * axis.x + transform.forward * axis.y) * Time.deltaTime;
                rig.position = new Vector3(rig.position.x, 0, rig.position.z);
            }
        }
	}
}
