using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPositioner : MonoBehaviour
{

    private GameObject player;
    private bool isColliding;
    [SerializeField] public float updateFrequency;
    [SerializeField] public float teleportRange;
    [SerializeField] public float protectiveRange;
    private float groundLevelHeight;

    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        isColliding = false;
        StartCoroutine(UpdatePosition());
        groundLevelHeight = 1.8369f; //y-position if person is on ground level
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
            do
            {
                SetNewPosition();
            } while (isColliding);

            //TODO
            //Obscure shadow from view before moving

            //wait for updateFrequency seconds
            yield return new WaitForSeconds(3.0f);
        }
    }

    void SetNewPosition()
    {
        //get player position
        Vector3 playerPos = player.transform.position;

        //set shadow to be same y as player
        if (playerPos.y <= groundLevelHeight) //checks if player is on ground level
        {
            this.gameObject.transform.position = new Vector3(
            playerPos.x + Random.Range(protectiveRange, teleportRange) * ((Random.Range(0, 2) == 0) ? 1 : -1),
            playerPos.y,
            playerPos.z + Random.Range(protectiveRange, teleportRange) * ((Random.Range(0, 2) == 0) ? 1 : -1)
            );
        }
        else //player is above ground level, assumes that player is inside building
        {
            //if player is in hallway
            // 30% chance shadow is in hallway
            // 85% chance shadow is behind
            // 15% shadow is front
            // 70% chance shadow is in classroom
            //if player is in classroom
            //
        }

    }

    public

    bool IsPlayerGrounded()
    {
        if (player.transform.position.y <= 1.83) // is on ground
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