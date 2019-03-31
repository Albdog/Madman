using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableInteraction : MonoBehaviour {

    private BoxCollider boxCollider;
    private GameObject chair;
    private bool playerEntered = false;
    private bool moveChair = false;
    private Vector3 original;

    // Use this for initialization
    void Start () {
        gameObject.tag = "TableAndChair";

        boxCollider = GetComponent<BoxCollider>();
        boxCollider.center = new Vector3(-0.005496f, -0.042708f, -0.674008f);
        boxCollider.size = new Vector3(1.391395f, 1.497191f, 2.173971f);

        chair = new GameObject("Chair");
        chair.transform.parent = transform;

        Instantiate(chair, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(transform.position.x, transform.position.y, transform.position.z)));

        var children = GetComponentsInChildren<Transform>();
        foreach(var child in children) {
            if(child.name == "Cube" || child.name == "Cube.001" || child.name == "Legs.001" || child.name == "Legs.002") {
                child.parent = chair.transform;
            }
        }
        original = new Vector3(chair.transform.position.x, chair.transform.position.y, chair.transform.position.z);

        Vector3 moveTo = new Vector3(chair.transform.position.x + 1.25f, chair.transform.position.y, chair.transform.position.z);

        chair.transform.position = Vector3.MoveTowards(chair.transform.position, moveTo, 10f);
    }
	
	// Update is called once per frame
	void Update () {
        //FOR VR
        //if(playerEntered && (RightHandInput.tableActivate || LeftHandInput.tableActivate)) {
        if(playerEntered && Input.GetKeyUp(KeyCode.F)) {
            boxCollider.center = new Vector3(-0.005496f, -0.042708f, -0.103295f);
            boxCollider.size = new Vector3(1.391395f, 1.497191f, 1.032544f);
            
            moveChair = true;
        }

        if(moveChair) {
            chair.transform.position = Vector3.MoveTowards(chair.transform.position, original, 2.5f * Time.deltaTime);

            if(chair.transform.position == original) moveChair = false;
        }
    }

    void OnTriggerEnter(Collider other) {
        playerEntered = true;
    }

    void OnTriggerExit(Collider other) {
        playerEntered = false;
    }
}
