using UnityEngine;
using System.Collections;

public class AuditoryHallucinations : MonoBehaviour
{
    public static AuditoryHallucinations instance;

    public Voice[] baseVoices_m, baseVoices_f, layeredVoices_m, layeredVoices_f;
    public AudioSource[] audioSources;
    private float x_maxDistance;

    void Awake()
    {
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
        StartCoroutine(PlayVoices());
    }

    private IEnumerator PlayVoices()
    {
        StartCoroutine(PlayBaseVoice());
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f, 1.0f));
        StartCoroutine(PlayReverbVoice());
        yield return new WaitForSeconds(UnityEngine.Random.Range(2.0f, 3.0f));
        StartCoroutine(PlayLayeredVoice());
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
            yield return new WaitForSeconds(voice.clip.length + UnityEngine.Random.Range(1.0f, 3.0f));
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
            yield return new WaitForSeconds(voice.clip.length + UnityEngine.Random.Range(2.0f, 5.0f));
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
            yield return new WaitForSeconds(voice.clip.length + UnityEngine.Random.Range(5.0f, 9.0f));
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
}