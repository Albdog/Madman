using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeeMovement : MonoBehaviour {

    private Transform target;
    public float speed;
    private Vector3 speedRot = Vector3.right * 50f;

    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update () {
        transform.LookAt(target);

        float targetY = target.position.y + 0.8f;
        Vector3 head = new Vector3(target.position.x, targetY, target.position.z);

        transform.position = Vector3.MoveTowards(transform.position, head, speed * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            WindowTrigger.totalBees--;
            Destroy(gameObject);
        }
    }
}
