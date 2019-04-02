using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ShadowPositioner : MonoBehaviour
{

    private GameObject player;
    private SchizoBarManager schizoBarManager;
    private FirstPersonController fps;

    [SerializeField] private float timeBeforeActivation;
    [SerializeField] private BoxCollider[] schoolColliders;

    private Vector2 updateFrequencies;
    private Vector2 teleportRanges;
    private Vector2 protectiveRanges;

    private float currentTime;
    private bool isColliding;
    private bool isPlayerOutside;
    private bool isShadowOutside;
    private bool hasStarted;
    private bool enableMovement;

    private static class SVR
    {
        public static Vector2 c_uf = new Vector2(25f, 40f); //updateFrequencies
        public static Vector2 c_tr = new Vector2(41f, 22.15f); //teleportRange
        public static Vector2 c_pr = new Vector2(20f, 13.15f); //protectiveRange
        public static float protectiveLimit = 6f;
        public static float updateFreqMax = 5f;
    }

    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        schizoBarManager = player.GetComponent<SchizoBarManager>();
        //fps = player.GetComponent<FirstPersonController>();
        isColliding = false;
        isPlayerOutside = true;
        isShadowOutside = true;
        hasStarted = false;
        enableMovement = true;
        currentTime = 0;
        //weightedRandomizer = new WeightedRandomizer();
        updateFrequencies = SVR.c_uf;
        teleportRanges = SVR.c_tr;
        protectiveRanges = SVR.c_pr;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        CheckIfPlayerIsOutside();
        UpdatePositionerValues(schizoBarManager.GetSchizoLevel());
        //if (fps.enabledShadowMovement && !hasStarted)
        //{
        //    StartCoroutine(UpdatePosition());
        //    hasStarted = true;
        //}
        if ( !hasStarted )
        {
            //wait timeBeforeActivation seconds before starting positioner
            currentTime += Time.deltaTime;
            if ( currentTime > timeBeforeActivation)
            {
                StartCoroutine(UpdatePosition());
                hasStarted = true;
            }
        }       
    }

    private void UpdatePositionerValues(float schizoLevel)
    {
        float schizoLerp = schizoLevel / 100f;
        updateFrequencies = new Vector2(Mathf.Lerp(SVR.c_uf.x, SVR.updateFreqMax, schizoLerp), 
            Mathf.Lerp(SVR.c_uf.y, SVR.updateFreqMax, schizoLerp));
        teleportRanges = new Vector2(Mathf.Lerp(SVR.c_tr.x, 0, schizoLerp),
            Mathf.Lerp(SVR.c_tr.y, 0, schizoLerp));
        protectiveRanges = new Vector2(Mathf.Lerp(SVR.c_pr.x, SVR.protectiveLimit, schizoLerp), 
            Mathf.Lerp(SVR.c_pr.y, SVR.protectiveLimit, schizoLerp));
    }
    
    private IEnumerator UpdatePosition()
    {
        while (true)
        {
            if (enableMovement)
            {
                //update its position
                do
                {
                    SetNewPosition();
                    CheckIfPlayerIsOutside();
                    CheckIfShadowIsOutside();
                } while (isColliding || (isPlayerOutside != isShadowOutside));

                yield return new WaitForSeconds(updateFrequencies[isPlayerOutside ? 0 : 1]);
            }
        }
    }

    void SetNewPosition()
    {
        //get player position
        Vector3 playerPos = player.transform.position;
        
        this.gameObject.transform.position = new Vector3(
        playerPos.x + Random.Range(protectiveRanges[isPlayerOutside ? 0 : 1], teleportRanges[isPlayerOutside ? 0 : 1]) * ((Random.Range(0, 2) == 0) ? 1 : -1),
        playerPos.y,
        playerPos.z + Random.Range(protectiveRanges[isPlayerOutside ? 0 : 1], teleportRanges[isPlayerOutside ? 0 : 1]) * ((Random.Range(0, 2) == 0) ? 1 : -1)
        );
    }

    void CheckIfPlayerIsOutside()
    {
        bool tempVar = true;
        foreach (BoxCollider b in schoolColliders)
        {
            if (b.GetComponent<SchoolTrigger>().IsPlayerInside())
            {
                tempVar = false;
                break;
            }
        }
        isPlayerOutside = tempVar;
    }

    void CheckIfShadowIsOutside()
    {
        bool tempVar = true;
        foreach (BoxCollider b in schoolColliders)
        {
            if (b.bounds.Contains(transform.position))
            {
                tempVar = false;
                break;
            }
        }
        isShadowOutside = tempVar;
    }

    public void RemoveFromView()
    {
        Vector3 playerPos = player.transform.position;

        this.gameObject.transform.position = new Vector3(
        playerPos.x + Random.Range(protectiveRanges[isPlayerOutside ? 0 : 1], teleportRanges[isPlayerOutside ? 0 : 1]) * ((Random.Range(0, 2) == 0) ? 1 : -1),
        -30,
        playerPos.z + Random.Range(protectiveRanges[isPlayerOutside ? 0 : 1], teleportRanges[isPlayerOutside ? 0 : 1]) * ((Random.Range(0, 2) == 0) ? 1 : -1)
        );
    }

    public void EnableMovement()
    {
        enableMovement = true;
    }

    public void DisableMovement()
    {
        enableMovement = false;
    }

    void OnCollisionEnter()
    {
        //activates when shadow is colliding with another object
        //boolean is used for making sure that shadow does not spawn in weird places
        isColliding = true;
    }

    void OnCollisionExit()
    {
        isColliding = false;
    }
}
