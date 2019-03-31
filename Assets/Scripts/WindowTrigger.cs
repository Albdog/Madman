using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowTrigger : MonoBehaviour {

    [SerializeField] private GameObject bee;

    private bool playerEntered = false;
    private int maxBees = 50;
    public static int totalBees;
    private Animator windowAnimator;
    private Transform window;

    // Use this for initialization
    void Start() {
        totalBees = 0;
        windowAnimator = GetComponent<Animator>();

        gameObject.tag = "Window";

        var children = GetComponentsInChildren<Transform>();
        foreach(var child in children) {
            if(child.name == "Sphere.001") {
                window = child;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        //FOR VR
        //if(playerEntered && (RightHandInput.windowActivate || LeftHandInput.windowActivate)) {
        if(playerEntered && Input.GetKeyUp(KeyCode.F)) {
            float minPosX = gameObject.transform.position.x + 5f;
            float maxPosX = gameObject.transform.position.x - 5f;
            float minPosY = gameObject.transform.position.y + 5f;
            float maxPosY = gameObject.transform.position.y + 0.1f;

            while(totalBees < maxBees) {
                float posX = Random.Range(minPosX, maxPosX);
                float posY = Random.Range(minPosY, maxPosY);
                float posZ;
                if(window.rotation.x < 0) {
                    posZ = gameObject.transform.position.z - 5f;
                } else {
                    posZ = gameObject.transform.position.z + 5f;
                }

                Vector3 beePos = new Vector3(posX, posY, posZ);

                Instantiate(bee, beePos, Quaternion.Euler(new Vector3(0, 90, 0)));

                totalBees++;
            }

            windowAnimator.SetBool("isOpen", !windowAnimator.GetBool("isOpen"));
        }
    }

    void OnTriggerEnter(Collider other) {
        playerEntered = true;
    }

    void OnTriggerExit(Collider other) {
        playerEntered = false;
    }
}
