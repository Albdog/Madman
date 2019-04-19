using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMovement : MonoBehaviour {

    private Transform target;
    public float speed;
    private Vector3 speedRot = Vector3.right * 50f;

    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update() {
        transform.LookAt(target);

        float targetY = target.position.y + 0.8f;
        Vector3 head = new Vector3(target.position.x, target.position.y, target.position.z);

        transform.position = Vector3.MoveTowards(transform.position, head, speed * Time.deltaTime);
    }
}
