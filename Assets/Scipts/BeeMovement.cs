using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeeMovement : MonoBehaviour {

    public float followSpeed;
    private Transform target;
    private float buzzSpeed = 5;
    private Vector3 speedRot = Vector3.right * 50f;

    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update () {
        transform.LookAt(target);

        if(followSpeed != 0) {
            float targetY = target.position.y + 0.8f;
            Vector3 head = new Vector3(target.position.x, targetY, target.position.z);

            transform.position = Vector3.MoveTowards(transform.position, head, followSpeed * Time.deltaTime);
        } else {

        }
	}

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            followSpeed = 0;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            followSpeed = 5;
        }
    }
}
