using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlayerUtilities : MonoBehaviour {

    private GameObject player;
    [SerializeField] BoxCollider gameArea;
    private bool isColliding;
    private VRTK_SlideObjectControlAction[] playerSpeedControl;
    private Vector3[] teleportSpots;
    //private GameObject[] playerColliders;
    private bool hasFoundColliders;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerSpeedControl = GameObject.FindGameObjectWithTag("PlayerSpeedContainer").GetComponents<VRTK_SlideObjectControlAction>();
        isColliding = false;
        hasFoundColliders = false;
        teleportSpots = new[]
        {
            //outside
            new Vector3 (-38.8f, 1.02f, 53.8f),
            new Vector3 (-14.3f, 1.02f, -25.4f),
            new Vector3 (48.1f, 1.02f, -96.6f),
            new Vector3 (43.3f, 1.02f, -189.81f),
            new Vector3 (-76.4f, 1.02f, -189.81f),
            //inside
            new Vector3 (0f, 1.02f, -166.894f),
            new Vector3 (0f, 7.83f, -131.25f),
            new Vector3 (-4.4f, 1.02f, -130.2f),
            new Vector3 (153.3f, 1.02f, -16.15f),
            new Vector3 (77.21f, 1.02f, -7.79f)
        };
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(isColliding);
        /*
        if (!hasFoundColliders && (GameObject.Find("[VRTK][AUTOGEN][BodyColliderContainer]") != null) )
        {
            playerColliders = new GameObject[2] { GameObject.Find("[VRTK][AUTOGEN][BodyColliderContainer]"), GameObject.Find("[VRTK][AUTOGEN][FootColliderContainer]") };
            playerColliders[0].tag = "IgnoreCollider";
            playerColliders[1].tag = "IgnoreCollider";
            hasFoundColliders = true;
        }
        */
    }

    void SetNewPosition()
    {
        //TODO enable player to spawn in 2nd floor hehe
        //Vector3 newPos = new Vector3(Random.Range(gameArea.bounds.min.x, gameArea.bounds.max.x), 1.9f, Random.Range(gameArea.bounds.min.z, gameArea.bounds.max.z));
        // Debug.Log(newPos);
        player.transform.position = teleportSpots[Random.Range(0, teleportSpots.Length)];
    }

    public void Teleport()
    {

        SetNewPosition();
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

    /*
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.parent != this.transform.parent)
        {
            isColliding = true;
            //activates when player is colliding with another object
            //boolean is used for making sure that player does not spawn in weird places
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.transform.parent != this.transform.parent)
        {
            isColliding = false;
        }
    }
    */

    void OnTriggerEnter(Collider other)
    {
        /*
        //Debug.Log(other.gameObject.transform.parent);
        
        if ((gameObject.transform.root.tag != other.transform.root.tag) && other.transform.root.tag != "Terrain")
        {
            //Debug.Log("Collision - different root");
            //Debug.Log(other.transform.root);
            //Debug.Log(other.transform.root.tag);
            
            isColliding = true;
            //activates when player is colliding with another object
            //boolean is used for making sure that player does not spawn in weird places
        }
        else if (!other.isTrigger)
        {
           // Debug.Log("Collision - is Trigger");
            //Debug.Log(other.transform.root);
            //Debug.Log(other.transform.root.tag);
           
            isColliding = true;
        }
        */
    }

    void OnTriggerExit(Collider other)
    {
        /*
        if ((gameObject.transform.root.tag != other.transform.root.tag) && other.transform.root.tag != "Terrain")
        {
            isColliding = false;
        }
        else if (!other.isTrigger)
        {
            isColliding = false;
        }*/
    }
}
