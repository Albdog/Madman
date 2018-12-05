using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

    private Animator doorAnimator;
    private BoxCollider boxColllider;
    private bool playerEntered = false;

    void Start() {
        doorAnimator = GetComponent<Animator>();
        boxColllider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update() {
        if(playerEntered && Input.GetKeyUp(KeyCode.F)) {
            float minPosX = gameObject.transform.position.x + 5f;
            float maxPosX = gameObject.transform.position.x - 5f;
            float minPosY = gameObject.transform.position.y + 5f;
            float maxPosY = gameObject.transform.position.y + 0.1f;
            bool isOpen = doorAnimator.GetBool("isOpen");

            doorAnimator.SetBool("isOpen", !isOpen);

            if(isOpen) {
                boxColllider.center = new Vector3(-0.02271719f, 1.934388f, -0.00230373f);
                boxColllider.size = new Vector3(1.438364f, 3.768569f, 0.1842929f);
            } else {
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
