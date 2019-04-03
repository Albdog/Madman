using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using VRTK;

public class FadeManager : MonoBehaviour {
    public static FadeManager Instance { set; get; }
    
    private bool isInTransition;
    private float transition;
    private bool isShowing;
    private float duration;
    private float startTime;
    private bool beeContact = false;
    private bool shadowContact = false; 
    private float time;
    private int count = 0;
    private ShadowPositioner shadow;
    //private FirstPersonController fps;
    private PlayerUtilities playerUtilities;
    private SchizoBarManager schizoBar;

    private bool hasDisabledShadow;
    private bool hasTeleportedPlayer;

    private VRTK_HeadsetFade headsetFade;

    void Start() {
        Instance = this;
        shadow = GameObject.FindGameObjectWithTag("Shadow").GetComponent<ShadowPositioner>();
        //fps = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        playerUtilities = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUtilities>();
        headsetFade = FindObjectOfType<VRTK_HeadsetFade>();
        schizoBar = GameObject.FindGameObjectWithTag("Player").GetComponent<SchizoBarManager>();
        hasDisabledShadow = false;
        hasTeleportedPlayer = false;
    }

    void Update() {
        time = Time.time - startTime;

        if (beeContact)
        {
            if (time > 8f)
            {
                headsetFade.Unfade(3f);

                GameObject[] bees = GameObject.FindGameObjectsWithTag("Bee");
                for (int i = 0; i < bees.Length; i++)
                {
                    Destroy(bees[i]);
                    WindowTrigger.totalBees--;
                }

                if (schizoBar = this.GetComponent<SchizoBarManager>())
                {
                    schizoBar.ChangeSchizoLevel(5);
                }

                beeContact = false;
                count = 0;
            }
            else if (time > 5f)
            {
                headsetFade.Fade(Color.black, 0.5f);
            }
        }
        else if (shadowContact)
        {
            if (time > 4.5f)
            {
                playerUtilities.EnableMovement();
                shadow.EnableMovement();
                shadowContact = false;
                hasDisabledShadow = false;
                hasTeleportedPlayer = false;
            }
            else if (time > 3f)
            {
                headsetFade.Unfade(1f);
                if (!hasTeleportedPlayer)
                {
                    playerUtilities.Teleport();
                    hasTeleportedPlayer = true;
                }
            }
            else if (time > 1f)
            {
                playerUtilities.DisableMovement();
                headsetFade.Fade(Color.black, 0.2f);
                if (!hasDisabledShadow)
                {
                    shadow.DisableMovement();
                    shadow.RemoveFromView();
                    schizoBar.ChangeSchizoLevel(3.5f);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Bee")) {
            beeContact = true;

            count++;
            if(count == 1) {
                startTime = Time.time;
            }
        }

        if (other.CompareTag("Shadow"))
        {
            shadowContact = true;
            startTime = Time.time;
        }
    }
}
