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
    private FirstPersonController fps;

    private bool hasDisabledShadow;

    private VRTK_HeadsetFade headsetFade;

    void Start() {
        Instance = this;
        shadow = GameObject.FindGameObjectWithTag("Shadow").GetComponent<ShadowPositioner>();
        fps = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        headsetFade = FindObjectOfType<VRTK_HeadsetFade>();
        hasDisabledShadow = false;
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

                SchizoBarManager schizoBar;
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
            if (time == 1f)
            {
                //set speed to 0 idk how with vr
                headsetFade.Fade(Color.black, 0.2f);
                if (!hasDisabledShadow)
                {
                    shadow.DisableMovement();
                    shadow.RemoveFromView();
                    schizoBar.ChangeSchizoLevel(5);
                }
            }
            else if (time > 3f)
            {
                headsetFade.Unfade(1f);
            }
            else if (time > 4f)
            {
                shadow.EnableMovement();
                shadowContact = false;
                hasDisabledShadow = false;
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
