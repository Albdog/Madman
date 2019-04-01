using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SchizoBarManager : MonoBehaviour {

    public Slider schizoFill;
    private float currentSchizoLevel;
    public float maxSchizoLevel;

    public void ChangeSchizoLevel(float amount) {
        currentSchizoLevel += amount;
        currentSchizoLevel = Mathf.Clamp(currentSchizoLevel, 0, maxSchizoLevel);

        schizoFill.value = currentSchizoLevel / maxSchizoLevel;
    }

    public float GetSchizoLevel ()
    {
        return currentSchizoLevel;
    }
}
