using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolTrigger : MonoBehaviour
{
    private bool isPlayerInside = false;
    private bool isShadowInside = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
        else if (other.gameObject.CompareTag("Shadow"))
        {
            isShadowInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
        else if(other.gameObject.CompareTag("Shadow"))
        {
            isShadowInside = false;
        }
    }

    public bool IsPlayerInside()
    {
        return isPlayerInside;
    }

    public bool IsShadowInside()
    {
        return isShadowInside;
    }
}