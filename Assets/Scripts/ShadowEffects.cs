using UnityEngine;
using UnityEngine.PostProcessing;

public class ShadowEffects : MonoBehaviour
{
    [SerializeField] public PostProcessingProfile profile;
    private GameObject player;
    private GameObject shadow;
    private SchizoBarManager schizoBar;
    private ShadowPositioner shadowPositioner;

    private ChromaticAberrationModel.Settings chrome;
    private GrainModel.Settings grain;
    private MotionBlurModel.Settings blur;
    private ColorGradingModel.Settings color;

    private AudioSource audioSource;

    const float MIN_DISTANCE = 18f;
    //const float MAX_TRANSITION_TIME = 2.0f;
    //private float timer;
    private bool isNearToShadow;
    private Vector2 damageRange = new Vector2(0.007f, 0.175f);

    private static class PPVRanges
    {
        public static Vector2 colorRedMixer = new Vector2(1f, 2f);
        public static Vector2 colorGreenMixer = new Vector2(0f, 1f);
        public static Vector2 colorSaturation = new Vector2(1f, 0.4f);
        public static Vector2 chromeIntensity = new Vector2(0f, 1f);
        public static Vector2 grainIntensity = new Vector2(0.1f, 0.8f);
        public static Vector2 grainSize = new Vector2(1f, 1.75f);
        public static Vector2 motionAngle = new Vector2(1f, 360f);
        public static Vector2 frameBlending = new Vector2(0f, 1f);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        shadow = GameObject.FindGameObjectWithTag("Shadow");
        schizoBar = player.GetComponent<SchizoBarManager>();
        shadowPositioner = shadow.GetComponent<ShadowPositioner>();
        GetNewReferences();
        //isNearToShadow = false;
        //timer = 0;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, shadow.transform.position);

        float lerp = 0; //sets lerp to 0 (case when shadow is out of affected bounds)
        if(distance < MIN_DISTANCE) //checks if player is close to shadow (case when shadow is in bounds)
        {
            shadowPositioner.DisableMovement();
            lerp = 1 - (distance / MIN_DISTANCE);
            //Debug.Log(lerp);
            if(lerp > 0.3f) {
                float damageLerp = (lerp - 0.3f) / 0.7f;
                schizoBar.ChangeSchizoLevel(Mathf.Lerp(damageRange.x, damageRange.y, damageLerp));
            }
        }
        else {
            shadowPositioner.EnableMovement();
        }

        GetNewReferences(); //gets the current value from main profile
        LerpAllValues(lerp); //accepts current lerp value and changes vision accordingly
        UpdateNewReferences(); //sets the values in main profile to new settings
        //Debug.Log(schizoBar.GetSchizoLevel());
    }

    //TODO smooth function?
    private void LerpAllValues(float lerp) 
    {
        chrome.intensity = Mathf.Lerp(PPVRanges.chromeIntensity.x, PPVRanges.chromeIntensity.y, lerp);
        color.channelMixer.red = new Vector3(
            Mathf.Lerp(PPVRanges.colorRedMixer.x, PPVRanges.colorRedMixer.y, lerp),
            Mathf.Lerp(PPVRanges.colorGreenMixer.x, PPVRanges.colorGreenMixer.y, lerp),
            color.channelMixer.red.z
            );
        color.basic.saturation = Mathf.Lerp(PPVRanges.colorSaturation.x, PPVRanges.colorSaturation.y, lerp);
        grain.intensity = Mathf.Lerp(PPVRanges.grainIntensity.x, PPVRanges.grainIntensity.y, lerp);
        grain.size = Mathf.Lerp(PPVRanges.grainSize.x, PPVRanges.grainSize.y, lerp);
        blur.shutterAngle = Mathf.Lerp(PPVRanges.motionAngle.x, PPVRanges.motionAngle.y, lerp);
        blur.frameBlending = Mathf.Lerp(PPVRanges.frameBlending.x, PPVRanges.frameBlending.y, lerp);

        audioSource.volume = Mathf.Lerp(0f, 0.16f, lerp);
    }

    private void GetNewReferences()
    {
        chrome = profile.chromaticAberration.settings;
        color = profile.colorGrading.settings;
        grain = profile.grain.settings;
        blur = profile.motionBlur.settings;
    }

    private void UpdateNewReferences()
    {
        profile.chromaticAberration.settings = chrome;
        profile.colorGrading.settings = color;
        profile.grain.settings = grain;
        profile.motionBlur.settings = blur;
    }

    private void OnApplicationQuit()
    {
        GetNewReferences();
        LerpAllValues(0);
        UpdateNewReferences();
    }
}