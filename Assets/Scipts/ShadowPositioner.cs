using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPositioner : MonoBehaviour {

    private GameObject player;
    private bool isColliding;
    [SerializeField] public float updateFrequency;
    [SerializeField] public float teleportRange;
    [SerializeField] public float protectiveRange;

	// Use this for initialization
	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
        isColliding = false;
        StartCoroutine(UpdatePosition());
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
    }

    private IEnumerator UpdatePosition()
    {
        while (true)
        {
            //update its position
            do {
                SetNewPosition();
            } while (isColliding);
               
            //TODO
            //Obscure shadow from view before moving
            //Check if player is on upper floors

            //wait for updateFrequency seconds
            yield return new WaitForSeconds(3.0f);
        }
    }

    void SetNewPosition()
    {
        //get player position
        Vector3 playerPos = player.transform.position;

        //set shadow to be same y as player
        this.gameObject.transform.position = new Vector3(
            playerPos.x + Random.Range(protectiveRange, teleportRange) * ((Random.Range(0, 2) == 0) ? 1 : -1),
            playerPos.y,
            playerPos.z + Random.Range(protectiveRange, teleportRange) * ((Random.Range(0, 2) == 0) ? 1 : -1)
            );

        //need to add control statement for checking if player is on ground
    }

    bool IsPlayerGrounded()
    {
        if ( player.transform.position.y <= 1.83 ) // is on ground
        {
            return true;
        }
        return false;
    }

    void OnCollisionEnter()
    {
        isColliding = true;
    }

    void OnCollisionExit()
    {
        isColliding = false;
    }
}
