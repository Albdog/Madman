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
        text.fontSize = 50;

        if (time > 5f) text.text = "Greetings. I'm John and I welcome you to Smart Labs. We're currently located in one of our remote testing sites and you're trying out our latest simulation on schizophrenic symptoms for a horror game we're working on. It's still a prototype, so we need to test out a bunch of features with a select few for our next iteration.";
        if(time > 17.5f) text.text = "Anyway, that's enough with introductions. I know you're eager to dive in already. Give me a second to boot up the simulation, this won't take too long. ";
        if(time > 22.5f) text.text = "Now, while you're on the way there, I guess I should talk to you first about schizophrenia, since this is a simulation.";
        if(time > 27.5f) text.text = "I'm sure you're well aware that patients afflicted by schizophrenia have problems understanding reality. Sometimes, they see things that aren't really there, and that scares them. Other times, they start believing false things, like them being hunted by a secret society. What most people seem to forget is that they're people who are suffering, just like people with appendicitis or cancer.";
        if(time > 40f) text.text = "Patients with schizophrenia experience positive and negative symptoms.";
        if(time > 42.5f) text.text = "Positive Symptoms:\n" + "Hallucinations, delusions, formal thought disorder, and abnormalities and motor behavior represent excessive or distorted bodily functions, distorting one's sense of reality";
        if(time > 52.5f) text.text = "Negative Symptoms:\n" + "Alogia, affective flattening, anhedonia, and anosgnosia essentially make a person function regressively";
        if(time > 62.5f) text.text = "It is important to understand that not all patients have the same symptoms. What you'll be experiencing in this simulation is merely a small set of the positive symptoms a person with schizophrenia may experience.";
        if(time > 70f) text.text = "It's easy to build a wall between us and them because of how they're usually portrayed. We have this idea that they're dangerous individuals, but really, they're the ones who are at most risk of being harmed.";
        if(time > 80f) text.text = "Anyway, sorry for rambling. Now, patients with schizophrenia also have other symptoms that affect their way of thinking, but these are hard to simulate without directly messing with your brain. So don't worry, you shouldn't experience any aftereffects at all after this experiment. As for the scary stuff... well, you'll know when you see it. Don't worry, they can't hurt you.";
        if(time > 90f) SceneManager.LoadScene(1);
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
