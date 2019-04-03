using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour {

    private AudioSource audioSource;
    [SerializeField] AudioClip[] effects;
    private Dictionary<string, AudioClip> effectsDict;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        effectsDict = new Dictionary<string, AudioClip>
        {
            { "door close", effects[0] },
            { "door open", effects[1] },
            { "fix chair", effects[2] },
            { "window close", effects[3] },
            { "breaker fix", effects[4] },
            { "switch flashlight", effects[5] },
            { "switch phone", effects[6] }
        };
    }

    public void PlaySoundEffect(string title)
    {
        if (effectsDict.ContainsKey(title))
        {
            audioSource.PlayOneShot(effectsDict[title], 1.0f);
        }
    }
}
