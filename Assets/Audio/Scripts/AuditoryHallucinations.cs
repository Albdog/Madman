using UnityEngine;
using System.Collections;

public class AuditoryHallucinations : MonoBehaviour
{
    public static AuditoryHallucinations instance;
    private GameObject player;
    private SchizoBarManager schizoBar;

    public Voice[] baseVoices_m, baseVoices_f, layeredVoices_m, layeredVoices_f;
    public AudioSource[] audioSources;
    private float x_maxDistance;
    private bool hasStartedBase;
    private bool hasStartedReverb;
    private bool hasStartedLayered;
    private Vector2 baseFreq;
    private Vector2 reverbFreq;
    private Vector2 layeredFreq;

    private static class AVR
    {
        public static Vector2 b_1 = new Vector2(1.5f, 3.2f); //frequency of base
        public static Vector2 b_2 = new Vector2(2.5f, 6f); //frequency of base
        public static Vector2 r_1 = new Vector2(1.7f, 3.7f); //frequency of base
        public static Vector2 r_2 = new Vector2(3.5f, 6f); //frequency of base
        public static Vector2 l_1 = new Vector2(5f, 9f); //frequency of base
        public static Vector2 l_2 = new Vector2(10f, 15f); //frequency of base
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        schizoBar = player.GetComponent<SchizoBarManager>();
        hasStartedBase = false;
        hasStartedReverb = false;
        hasStartedLayered = false;

        // singleton
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        InitializeVoices();
    }

    private void Update()
    {
        float schizoLevel = schizoBar.GetSchizoLevel();
        UpdateFrequencyValues(schizoLevel);
        if(!hasStartedBase && schizoLevel > 10f) {
            StartCoroutine(PlayBaseVoice());
            hasStartedBase = true;
        }
        if(!hasStartedReverb && schizoLevel > 25f) {
            StartCoroutine(PlayReverbVoice());
            hasStartedReverb = true;
        }
        if(!hasStartedLayered && schizoLevel > 40f) {
            StartCoroutine(PlayLayeredVoice());
            hasStartedLayered = true;
        }
    }

    private IEnumerator PlayBaseVoice()
    {
        while (true)
        {
            RandomizeSourcePosition(0);
            Voice voice = GetBaseVoice("base");
            voice.source = audioSources[0];
            voice.source.clip = voice.clip;
            voice.source.Play();
            yield return new WaitForSeconds(voice.clip.length + UnityEngine.Random.Range(baseFreq.x, baseFreq.y));
        }
    }

    private IEnumerator PlayReverbVoice()
    {
        while (true)
        {
            RandomizeSourcePosition(1);
            Voice voice = GetBaseVoice("base");
            voice.source = audioSources[1];
            voice.source.clip = voice.clip;
            voice.source.Play();
            yield return new WaitForSeconds(voice.clip.length + UnityEngine.Random.Range(reverbFreq.x, reverbFreq.y));
        }
    }

    private IEnumerator PlayLayeredVoice()
    {
        while (true)
        {
            RandomizeSourcePosition(2);
            Voice voice = GetBaseVoice("layered");
            voice.source = audioSources[2];
            voice.source.Play();
            yield return new WaitForSeconds(voice.clip.length + UnityEngine.Random.Range(layeredFreq.x, layeredFreq.y));
        }
    }

    /* Helper Functions below */

    Voice GetBaseVoice(string type)
    {
        if (type == "base")
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                return baseVoices_m[UnityEngine.Random.Range(0, baseVoices_m.Length)];
            }
            else
            {
                return baseVoices_f[UnityEngine.Random.Range(0, baseVoices_f.Length)];
            }
        }
        else if (type == "layered")
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                return layeredVoices_m[UnityEngine.Random.Range(0, layeredVoices_m.Length)];
            }
            else
            {
                return layeredVoices_f[UnityEngine.Random.Range(0, layeredVoices_f.Length)];
            }
        }
        return null;
    }

    private void InitializeVoices()
    {
        foreach (Voice v in baseVoices_m)
        {
            v.source = audioSources[0];
            v.source.clip = v.clip;
            v.source.loop = v.loop;
        }
        foreach (Voice v in baseVoices_f)
        {
            v.source = audioSources[0];
            v.source.clip = v.clip;
            v.source.loop = v.loop;
        }
        foreach (Voice v in layeredVoices_m)
        {
            v.source = audioSources[2];
            v.source.clip = v.clip;
            v.source.loop = v.loop;
        }
        foreach (Voice v in layeredVoices_f)
        {
            v.source = audioSources[2];
            v.source.clip = v.clip;
            v.source.loop = v.loop;
        }
    }

    void ResetSourcePosition(int num)
    {
        audioSources[num].transform.localPosition = Vector3.zero;
    }

    void RandomizeSourcePosition(int num)
    {
        ResetSourcePosition(num);
        //create transform values and apply it to source
        Vector3 newPosition = new Vector3(
            UnityEngine.Random.Range(-2.8f, 2.8f),
            UnityEngine.Random.Range(-0.5f, 2.8f),
            UnityEngine.Random.Range(-2.8f, 2.8f));
        audioSources[num].transform.localPosition = newPosition;
    }

    private void UpdateFrequencyValues(float schizoLevel)
    {
        float schizoLerp = schizoLevel / 100f;

        baseFreq = new Vector2(Mathf.Lerp(AVR.b_1.x, AVR.b_2.x, schizoLerp),
            Mathf.Lerp(AVR.b_1.y, AVR.b_2.y, schizoLerp));
        reverbFreq = new Vector2(Mathf.Lerp(AVR.r_1.x, AVR.r_2.x, schizoLerp),
            Mathf.Lerp(AVR.r_1.y, AVR.r_2.y, schizoLerp));
        layeredFreq = new Vector2(Mathf.Lerp(AVR.l_1.x, AVR.l_2.x, schizoLerp),
            Mathf.Lerp(AVR.l_1.y, AVR.l_2.y, schizoLerp));
    }
}