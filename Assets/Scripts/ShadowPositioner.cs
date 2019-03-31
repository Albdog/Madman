using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ShadowPositioner : MonoBehaviour
{

    private GameObject player;
    private SchizoBarManager schizoBarManager;
    private FirstPersonController fps;

    [SerializeField] public float[] updateFrequencies;
    [SerializeField] public float[] teleportRanges;
    [SerializeField] public float[] protectiveRanges;
    [SerializeField] public BoxCollider[] schoolColliders;
    [SerializeField] public float[] levelHeights;
    private bool isColliding;
    private bool isPlayerOutside;
    private bool isShadowOutside;
    private bool hasStarted;
    private bool enableMovement;

    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        schizoBarManager = player.GetComponent<SchizoBarManager>();
        fps = player.GetComponent<FirstPersonController>();
        isColliding = false;
        isPlayerOutside = true;
        isShadowOutside = true;
        hasStarted = false;
        enableMovement = true;
        //weightedRandomizer = new WeightedRandomizer();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        CheckIfPlayerIsOutside();
        if (fps.enabledShadowMovement && !hasStarted)
        {
            StartCoroutine(UpdatePosition());
            hasStarted = true;
        }
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

                //dont do it when isColliding || 
                //TODO
                //Obscure shadow from view before moving

                yield return new WaitForSeconds(updateFrequencies[isPlayerOutside ? 0 : 1]);
                //yield return new WaitForSeconds(3.0f);
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

    /*
     * Only helper functions below. :--)
     
    private class Option
    {
        private int chance;
        private string name;

        public Option(string x, int y)
        {
            name = x;
            chance = y;
        }

        public string getName() { return name; }
        public void setName(string newName) { name = newName; }
        public int getChance() { return chance; }
        public void setChance(int newChance) { chance = newChance; }
    }

    private class WeightedRandomizer
    {
        public void updateWeights (float currentSchizoLevel)
        {
            //TODO set adjusting weights algo
        }

        public string Randomize(Option[] options)
        {
            int randomInt = Random.Range(0, 101);

            return "x";
        }
    }
    */
}
