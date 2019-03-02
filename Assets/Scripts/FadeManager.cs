using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class FadeManager : MonoBehaviour {
    public static FadeManager Instance { set; get; }
    
    private bool isInTransition;
    private float transition;
    private bool isShowing;
    private float duration;
    private float startTime;
    private bool beeContact = false;
    private float time;
    private int count = 0;

    private VRTK_HeadsetFade headsetFade;

    void Start() {
        Instance = this;

        headsetFade = FindObjectOfType<VRTK_HeadsetFade>();
    }

    void Update() {
        time = Time.time - startTime;

        if(beeContact) {
        	if(time > 8f) {
                headsetFade.Unfade(3f);

                GameObject[] bees = GameObject.FindGameObjectsWithTag("Bee");
                for(int i = 0; i < bees.Length; i++) {
                    Destroy(bees[i]);
                    WindowTrigger.totalBees--;
                }

                //SchizoBarManager schizoBar;
                //if(schizoBar = this.GetComponent<SchizoBarManager>()) {
                //    schizoBar.ChangeSchizoLevel(5);
                //}

                beeContact = false;
                count = 0;
            }
            else if(time > 5f) {
                headsetFade.Fade(Color.black, 0.5f);
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
    }
}
