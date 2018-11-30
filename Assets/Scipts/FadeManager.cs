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

    void Awake() {
        Instance = this;
        startTime = Time.time;
        print("awake");
    }

    public void Fade(bool showing, float duration) {
        isShowing = showing;
        isInTransition = true;
        this.duration = duration;
        transition = (isShowing) ? 0 : 1;
    }

    void Update() {
        //fix update not being called
        if(!isInTransition) {
            return;
        }

        print("update");

        if(beeContact) {
            if(time > 6.5f) {
                print("white");
                Fade(false, 3f);
            }
            else if(time > 5f) {
                print("black");
                Fade(true, 1.5f);
            }
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
                print(count);
                time = Time.time - startTime;
            }
        }
    }
}
