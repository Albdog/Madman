using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] concreteSounds;
    [SerializeField]
    private AudioClip[] dirtSounds;
    [SerializeField]
    private AudioClip[] grassSounds;

    private AudioSource audioSource;
    private TerrainDetector terrainDetector;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        terrainDetector = new TerrainDetector();
    }

    public void Step()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        int terrainTextureIndex = terrainDetector.GetActiveTerrainTextureIdx(transform.position);

        switch(terrainTextureIndex)
        {
            case 0:
                return concreteSounds[UnityEngine.Random.Range(0, concreteSounds.Length)];
            case 1:
                return dirtSounds[UnityEngine.Random.Range(0, dirtSounds.Length)];
            case 2:
            default:
                return grassSounds[UnityEngine.Random.Range(0, grassSounds.Length)];
        }
        
    }
}