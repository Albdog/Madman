using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour {

    public Text text;
    public AudioClip[] openingScene, endingScene;
    private AudioSource scientist;
    private bool[] isPlayingOS, isPlayingES;
    private float time, startTime;

    // Use this for initialization
    void Start () {
        GameState.isGameOver = false;
        startTime = Time.time;
        scientist = gameObject.GetComponent<AudioSource>();
        isPlayingOS = new bool[openingScene.Length];
        isPlayingES = new bool[endingScene.Length];
        text.fontSize = 50;
    }

	// Update is called once per frame
	void Update () {
        if(!GameState.isGameOver) OpeningScene();
        else EndingScene();

        //EndingScene();
    }

    void OpeningScene() {
        time = Time.time - startTime;

        if(time > 0f) {
            if(!isPlayingOS[0]) {
                scientist.PlayOneShot(openingScene[0]);
                isPlayingOS[0] = true;
            }
            text.text = "Greetings. I welcome you to Smart Labs. We're currently located in one of our remote testing sites and you're trying out our latest simulation on schizophrenic symptoms for a horror game we're working on. It's still a prototype, so we need to test out a bunch of features with a select few for our next iteration.";
        }
        if(time > 20f) {
            if(!isPlayingOS[1]) {
                scientist.PlayOneShot(openingScene[1]);
                isPlayingOS[1] = true;
            }
            text.text = "Now, while our devices are still booting up, I guess I should talk to you first about schizophrenia, since this is a simulation.";
        }
        if(time > 29f) {
            if(!isPlayingOS[2]) {
                scientist.PlayOneShot(openingScene[2]);
                isPlayingOS[2] = true;
            }
            text.text = "I'm sure you're well aware that patients with schizophrenia have trouble understanding reality. Sometimes, they see things that aren't really there, and that scares them. Other times, they start believing false things, like them being hunted by a secret society. What most people seem to forget is that they are people who are suffering, just like people with appendicitis or cancer. ";
        }

        if(time > 52f) {
            if(!isPlayingOS[3]) {
                scientist.PlayOneShot(openingScene[3]);
                isPlayingOS[3] = true;
            }
            text.text = "Patients with schizophrenia experience positive and negative symptoms.";
        }
        if(time > 57f) text.text = "Positive Symptoms:\n" + "Hallucinations, delusions, formal thought disorder, and abnormalities and motor behavior represent excessive or distorted bodily functions, distorting one's sense of reality";
        if(time > 71f) text.text = "Negative Symptoms:\n" + "Alogia, affective flattening, anhedonia, and anosgnosia essentially make a person function regressively";
        if(time > 82f) text.text = "It is important to understand that not all patients have the same symptoms. What you'll be experiencing in this simulation is merely a small set of the positive symptoms a person with schizophrenia may experience. ";


        if(time > 95f) {
            if(!isPlayingOS[4]) {
                scientist.PlayOneShot(openingScene[4]);
                isPlayingOS[4] = true;
            }
            text.text = "Now, patients with schizophrenia also have other symptoms that affect their way of thinking, but these are hard to simulate without directly messing with your brain. So don't worry, you'll be fine after this. As for the scary stuff... well, you'll know when you see it. Don't worry, they can't hurt you.";
        }

        if(time > 114f) text.text = "Loading simulation...";
        if(time > 119f) SceneManager.LoadScene(1);
    }

    void EndingScene() {
        time = Time.time - startTime;

        if(time > 0f) {
            string o = "Welcome back.\n";
            if(GameState.isWin) {
                o = o + "Well done, you passed the experiment.";
                if(!isPlayingES[1]) {
                    scientist.PlayOneShot(endingScene[1]);
                    isPlayingES[1] = true;
                }
            }
            else {
                o = o + "Unfortunately, you failed the experiment.";
                if(!isPlayingES[0]) {
                    scientist.PlayOneShot(endingScene[0]);
                    isPlayingES[0] = true;
                }
            }

            text.text = o;
        }

        if(time > 5f) {
            if(!isPlayingES[2]) {
                scientist.PlayOneShot(endingScene[2]);
                isPlayingES[2] = true;
            }
            text.text = "The type of schizophrenia we portrayed in the experiment is only one variation out of many. It does not represent schizophrenia as a whole. Things are not as simple as they seem.";
        }

        if(time > 18f) {
            if(!isPlayingES[3]) {
                scientist.PlayOneShot(endingScene[3]);
                isPlayingES[3] = true;
            }
            text.text = "It's easy to build a wall that separates us from patients with schizophrenia because they're different. They are normally portrayed as dangerous individuals, but in reality, they are the ones who are at risk.";
        }

        if(time > 31f) {
          if(!isPlayingES[4]) {
              scientist.PlayOneShot(endingScene[4]);
              isPlayingES[4] = true;
          }
            text.text = "They are at risk of living alone, rejected from society. They are at risk of not being able to get proper help. They are at risk from themselves. They do not wish to hurt you. They only wish to be normal.";
        }

        if(time > 46f) {
            if(!isPlayingES[5]) {
                scientist.PlayOneShot(endingScene[5]);
                isPlayingES[5] = true;
            }
            text.text = "Always remember that they are humans as well. They did not ask for whatever they are experiencing. Keep your mind and heart open, and be understanding. Be kind, always.";
        }

        if(time > 59f) {
            if(!isPlayingES[6]) {
                scientist.PlayOneShot(endingScene[6]);
                isPlayingES[6] = true;
            }
            text.text = "Thanks for playing.";
        }
    }
}
