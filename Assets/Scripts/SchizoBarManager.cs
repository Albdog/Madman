using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SchizoBarManager : MonoBehaviour {

    public Slider schizoFill;
    private float currentSchizoLevel;
    public float maxSchizoLevel;

    public void ChangeSchizoLevel(int amount) {
        currentSchizoLevel += amount;
        currentSchizoLevel = Mathf.Clamp(currentSchizoLevel, 0, maxSchizoLevel);

        schizoFill.value = currentSchizoLevel / maxSchizoLevel;
    }

    public float getSchizoLevel ()
    {
        return currentSchizoLevel;
    }

    void Update() {
        if(currentSchizoLevel >= maxSchizoLevel) {
            GameState.isGameOver = true;
            GameState.isGameOver = false;
            SceneManager.LoadScene(0);
        }
    }
}
