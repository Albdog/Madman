using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour {
    public static FadeManager Instance { set; get; }

    public Image fadeImage;
    private bool isInTransition;
    private float transition;
    private bool isShowing;
    private float duration;
    private float startTime;
    private bool beeContact = false;
    private float time;
    private int count = 0;

    void Start() {
        Instance = this;
    }

    public void Fade(bool showing, float duration) {
        isShowing = showing;
        isInTransition = true;
        this.duration = duration;
        transition = (isShowing) ? 0 : 1;
    }

    void Update() {
        time = Time.time - startTime;

        if(beeContact) {
            if(time > 7f && isShowing) {
                Fade(false, 3f);

                GameObject[] bees = GameObject.FindGameObjectsWithTag("Bee");
                for(int i = 0; i < bees.Length; i++) {
                    Destroy(bees[i]);
                    WindowTrigger.totalBees--;
                }

                SchizoBarManager schizoBar;
                if(schizoBar = this.GetComponent<SchizoBarManager>()) {
                    schizoBar.ChangeSchizoLevel(5);
                }

                beeContact = false;
                count = 0;
            }
            else if(time > 5f && !isShowing) {
                Fade(true, 1.5f);
            }
        }

        if(!isInTransition) {
            return;
        }

        transition += (isShowing) ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
        fadeImage.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);

        if(transition > 1 || transition < 0)
            isInTransition = false;
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
