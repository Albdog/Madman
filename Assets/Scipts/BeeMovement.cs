using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeeMovement : MonoBehaviour {

    public float followSpeed;
    public Transform target;
    private float buzzSpeed = 2.5f;
    private bool isFollowing = true;
    private Vector3 speedRot = Vector3.right * 50f;

    private void Start() {
        target = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update() {
        transform.LookAt(target);

        if(isFollowing) {
            Vector3 player = target.position;

            transform.position = Vector3.MoveTowards(transform.position, player, followSpeed * Time.deltaTime);
        }
        else {
            float x = Random.Range(target.position.x - 50f, target.position.x + 50f);
            float y = Random.Range(target.position.y - 15f, target.position.y + 15f);
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
