using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

    private Animator doorAnimator;
    private BoxCollider boxColllider;
    private bool playerEntered = false;
    private SoundEffectsManager sfx;

    void Start() {
        doorAnimator = GetComponent<Animator>();
        boxColllider = GetComponent<BoxCollider>();
        sfx = GameObject.FindGameObjectWithTag("SoundEffects").GetComponent<SoundEffectsManager>();
    }
    
    void Update() {
        if(playerEntered && (RightHandInput.doorActivate || LeftHandInput.doorActivate)) {
            bool isOpen = doorAnimator.GetBool("isOpen");

            doorAnimator.SetBool("isOpen", !isOpen);

            if(isOpen) {
                sfx.PlaySoundEffect("door close");
                boxColllider.center = new Vector3(-0.02271719f, 1.934388f, -0.00230373f);
                boxColllider.size = new Vector3(1.438364f, 3.768569f, 0.1842929f);
            } else {
                sfx.PlaySoundEffect("door open");
                boxColllider.center = new Vector3(0.6447802f, 1.934389f, -0.5744121f);
                boxColllider.size = new Vector3(0.1033757f, 3.768569f, 1.32851f);
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
