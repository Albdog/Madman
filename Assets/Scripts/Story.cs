using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour {

    public Text text;
    private float time, startTime;

    // Use this for initialization
    void Start () {
        GameState.isGameOver = false;
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if(!GameState.isGameOver) OpeningScene();
        else EndingScene();

        //EndingScene();
    }

    void OpeningScene() {
        time = Time.time - startTime;

        if(time > 5f) SceneManager.LoadScene(1);
    }

    void EndingScene() {
        time = Time.time - startTime;
        text.fontSize = 50;

        if(time < 5f) {
            string o = "Welcome back.\n";
            if(GameState.isWin) {
                o = o + "Congratulations, you passed the experiement.";
            }
            else {
                o = o + "Unfortunately, you failed the experiment.";
            }

            text.text = o;
        }
        if(time > 5f) text.text = "The type of schizophrenia we portrayed in the experiment is only one variation out of many. It does not represent schizophrenia as a whole. Things are not as simple as they seem.";
        if(time > 15f) text.text = "It's easy to build a wall that separates us from patients with schizophrenia because they're different. They are normally portrayed as dangerous individuals, but in reality, they are the ones who are at risk.";
        if(time > 25f) text.text = "They are at risk of living alone, rejected from society. They are at risk of not being able to get proper help. They are at risk from themselves. They do not wish to hurt you. They only wish to be normal.";
        if(time > 35f) text.text = "Always remember that they are humans as well. They did not ask for whatever they are experiencing. Keep your mind and heart open, and be understanding. Be kind, always.";
        if(time > 45f) text.text = "Thanks for playing.";
    }
}
