using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlayerUtilities : MonoBehaviour {

    private GameObject player;
    [SerializeField] BoxCollider gameArea;
    private bool isColliding;
    private VRTK_SlideObjectControlAction[] playerSpeedControl;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("PlayerSpeedContainer");
        playerSpeedControl = player.GetComponents<VRTK_SlideObjectControlAction>();
        isColliding = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetNewPosition()
    {
        //get player position
        Vector3 playerPos = player.transform.position;

        //TODO enable player to spawn in 2nd floor hehe
        player.transform.position = new Vector3(
        playerPos.x + Random.Range(gameArea.bounds.min.x, gameArea.bounds.max.x),
        playerPos.y,
        playerPos.z + Random.Range(gameArea.bounds.min.z, gameArea.bounds.max.z)
        );
    }

    public void Teleport()
    {
        while ( isColliding )
        {
            SetNewPosition();
        }
    }

    public void EnableMovement()
    {
        playerSpeedControl[0].maximumSpeed = 5f;
        playerSpeedControl[1].maximumSpeed = 5f;
    }

    public void DisableMovement()
    {
        playerSpeedControl[0].maximumSpeed = 0f;
        playerSpeedControl[1].maximumSpeed = 0f;
    }

    void OnCollisionEnter()
    {
        //activates when player is colliding with another object
        //boolean is used for making sure that player does not spawn in weird places
        isColliding = true;
    }

    void OnCollisionExit()
    {
        isColliding = false;
    }
}
