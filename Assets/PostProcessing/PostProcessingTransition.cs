using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcessingTransition : MonoBehaviour
{
    [SerializeField] public PostProcessingProfile profile;
    private GameObject player;
    private GameObject shadow;

    private BloomModel.Settings bloom;
    private ChromaticAberrationModel.Settings chrome;
    private GrainModel.Settings grain;
    private MotionBlurModel.Settings blur;

    const float MAX_DISTANCE = 18f;

    private static class Ranges
    {
        private static float[] bloomRadius = { 4f, 6.5f };
        private static float[] bloomIntensity = { 1f, 15f };
        private static float[] chromeIntensity = { 0f, 1f };
        private static float[] grainIntensity = { 0f, 0.8f };
        private static float[] grainSize = { 1f, 1.75f };
        private static float[] motionAngle = { 1f, 360f };
        private static float[] frameBlending = { 0f, 1f };
        private static float[] playerShadowDistance = { 18f, 0.5f };
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shadow = GameObject.FindGameObjectWithTag("Shadow");
        bloom = profile.bloom.settings;
        chrome = profile.chromaticAberration.settings;
        grain = profile.grain.settings;
        blur = profile.motionBlur.settings;
    }

    private float GetDistanceBetween(Vector3 playerPos, Vector3 shadowPos)
    {
        return (playerPos - shadowPos).sqrMagnitude;
    }

    void Update()
    {
        float distance = GetDistanceBetween(player.transform.position, shadow.transform.position);
        if ( distance > MAX_DISTANCE)
        {
            float lerp = distance / MAX_DISTANCE;
        }
        else
        {

        }
        
        /*
        ChromaticAberrationModel.Settings chrome = profile.chromaticAberration.settings;
        if (Player.GetComponent<PlayerMove>().isSprinting == true)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, runningFov, Time.deltaTime * runChangeSpeed);
            chrome.intensity = Mathf.Lerp(chrome.intensity, 1, Time.deltaTime * Routes.range);
            profile.chromaticAberration.settings = chrome;


        }
        else if (Player.GetComponent<PlayerMove>().isSprinting == false && GetComponent<Camera>().fieldOfView != 60f)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, 60f, Time.deltaTime * walkChangeSpeed);
            chrome.intensity = Mathf.Lerp(chrome.intensity, 0, Time.deltaTime * runChangeSpeed);
            profile.chromaticAberration.settings = chrome;
        }
        */
    }
}