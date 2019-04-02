using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSwitchInteraction : MonoBehaviour {

    public static bool isDisabled;
    private bool playerEntered;

    // Use this for initialization
    void Start () {
        isDisabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        //FOR VR
        if(playerEntered && (LeftHandInput.mainSwitchActivate || RightHandInput.mainSwitchActivate) && (!isDisabled)) isDisabled = true;

        if(playerEntered && (LeftHandInput.mainSwitchActivate || RightHandInput.mainSwitchActivate) && GameState.areFuseBoxesFixed) {
            GameState.isGameOver = true;
            GameState.isWin = true;
        }

        /*if(playerEntered && Input.GetKeyUp(KeyCode.F) && (!isDisabled)) isDisabled = true;

        if(playerEntered && Input.GetKeyUp(KeyCode.F) && GameState.areFuseBoxesFixed) {
            GameState.isGameOver = true;
            GameState.isWin = true;
            SceneManager.LoadScene(0);
        }*/
    }

    void OnTriggerEnter(Collider other) {
        playerEntered = true;
    }

    void OnTriggerExit(Collider other) {
        playerEntered = false;
    }
}
