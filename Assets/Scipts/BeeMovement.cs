using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeeMovement : MonoBehaviour {

    public float followSpeed;
    private Transform target;
    private float buzzSpeed = 5;
    private bool isFollowing = true;
    private Vector3 speedRot = Vector3.right * 50f;

    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update() {
        transform.LookAt(target);
        float targetY = target.position.y + 0.8f;

        if(isFollowing) {
            Vector3 head = new Vector3(target.position.x, targetY, target.position.z);

            transform.position = Vector3.MoveTowards(transform.position, head, followSpeed * Time.deltaTime);
        }
        else {
            float x = Random.Range(target.position.x - 50f, target.position.x + 50f);
            float y = Random.Range(targetY - 15f, targetY + 15f);
            float z = Random.Range(target.position.z - 50f, target.position.z + 50f);

            Vector3 flyTo = new Vector3(x, y, z);

            transform.position = Vector3.MoveTowards(transform.position, flyTo, buzzSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            followSpeed = 0;
            isFollowing = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            followSpeed = 5;
            isFollowing = true;
        }
    }
}
