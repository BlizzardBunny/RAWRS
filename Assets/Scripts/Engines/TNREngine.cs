using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNREngine : MonoBehaviour
{
    #region Object References
    [SerializeField] private SceneTransitions sceneTransitions;
    [SerializeField] private TMPro.TextMeshProUGUI nextDay;
    [SerializeField] private Transform player;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private DialogueEngine dialogueEngine;
    public GameObject[] dialogueStates;

    [SerializeField] private Transform eventTiles;
    [SerializeField] private GameObject CTAPrefab;
    [SerializeField] private Transform[] spawnPts;
    public GameObject failDialogue;

    [SerializeField] private GameObject taskEntryPrefab;
    [SerializeField] private Transform taskEntryParent;
    #endregion

    #region Variables
    private GameObject[] ctaList = new GameObject[3]; //Length defines spawn number
    private TaskEntry[] taskEntryList = new TaskEntry[3];
    #endregion

    #region Unity Functions
    // Start is called before the first frame update
    void Start()
    {
        nextDay.enabled = false;
        StaticItems.LoadGame();
        dialogueEngine.dialogueCanvas.enabled = true;
        dialogueEngine.StartDialogue(ref dialogueStates[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticItems.TNRstate == 1)
        {
            StaticItems.TNRstate++;
            StartCoroutine(WaitForFadeOut());
        }
        else if (StaticItems.TNRstate == 3)
        {
            StaticItems.TNRstate++;
            StartCoroutine(WaitForFadeIn()); 
            StaticItems.plrPos = new Vector3(110.5f, 1.5f, 0.0f);
            player.position = StaticItems.plrPos;
            playerAnim.SetInteger("direction", 1);
            dialogueEngine.dialogueCanvas.enabled = true;
            dialogueEngine.StartDialogue(ref dialogueStates[1]);
            SetupLevel();
        }
        else if (StaticItems.TNRstate == 7)
        {
            dialogueEngine.dialogueCanvas.enabled = true;
            dialogueEngine.StartDialogue(ref dialogueStates[2]);
        }
        else if (StaticItems.TNRstate == 8)
        {
            StaticItems.TNRstate++;
            StartCoroutine(WaitForFadeOut());
        }
        else if (StaticItems.TNRstate == 10)
        {
            StaticItems.TNRstate++;
            StartCoroutine(WaitForFadeIn());
            StaticItems.plrPos = new Vector3(2.5f, 17.5f, 0.0f);
            player.position = StaticItems.plrPos;
            playerAnim.SetInteger("direction", 3);
            dialogueEngine.dialogueCanvas.enabled = true;
            dialogueEngine.StartDialogue(ref dialogueStates[3]);
        }
        else if (StaticItems.TNRstate == 13)
        {
            StaticItems.TNRstate++;
            StartCoroutine(WaitForFadeOut());
        }
        else if (StaticItems.TNRstate == 15)
        {
            StaticItems.TNRstate++;
            StartCoroutine(WaitForFadeIn());
            dialogueEngine.dialogueCanvas.enabled = true;
            dialogueEngine.StartDialogue(ref dialogueStates[4]);
        }
        else if (StaticItems.TNRstate == 18)
        {
            nextDay.enabled = true;
            StaticItems.TNRstate++;
            StartCoroutine(WaitForFadeOut());
        }
        else if (StaticItems.TNRstate == 20)
        {
            nextDay.enabled = false;
            StaticItems.TNRstate++;
            StartCoroutine(WaitForFadeIn());
            StaticItems.plrPos = new Vector3(110.5f, 1.5f, 0.0f);
            player.position = StaticItems.plrPos;
            playerAnim.SetInteger("direction", 1);
            dialogueEngine.dialogueCanvas.enabled = true;
            dialogueEngine.StartDialogue(ref dialogueStates[5]);
        }
        else if (StaticItems.TNRstate == 24)
        {
            dialogueEngine.dialogueCanvas.enabled = true;
            dialogueEngine.StartDialogue(ref dialogueStates[6]);
            StaticItems.TNRstate++;
        }
    }
    #endregion

    #region Custom Functions
    void SetupLevel()
    {
        for (int i = 0; i < ctaList.Length; i++)
        {
            int rnd = Random.Range(0, spawnPts.Length - 1);

            if (i > 0)
            {
                while (!CheckIfUnique(rnd))
                {
                    rnd = Random.Range(0, spawnPts.Length - 1);
                }
            }

            ctaList[i] = Instantiate(CTAPrefab, spawnPts[rnd].position, Quaternion.identity, eventTiles);

            CatTrappingArea cta = ctaList[i].GetComponent<CatTrappingArea>();
            cta.index = i;
            cta.tNREngine = this.GetComponent<TNREngine>();

            GameObject taskEntry = Instantiate(taskEntryPrefab, taskEntryParent);
            taskEntryList[i] = taskEntry.GetComponent<TaskEntry>();
            taskEntryList[i].taskStationMarker = cta.markerArrow;
            taskEntryList[i].taskName.text = "Trap a Cat";
            taskEntryList[i].taskNumber.text = (i + 1).ToString();
        }
    }

    public void MarkAsDone(int index)
    {
        taskEntryList[index].numberBG.color = Color.green;
    }

    public void ReplaceCTAIndex(int index)
    {
        int rnd = Random.Range(0, spawnPts.Length - 1);

        while (!CheckIfUnique(rnd))
        {
            rnd = Random.Range(0, spawnPts.Length - 1);
        }

        ctaList[index] = Instantiate(CTAPrefab, spawnPts[rnd].position, Quaternion.identity, eventTiles);

        CatTrappingArea cta = ctaList[index].GetComponent<CatTrappingArea>();
        cta.index = index;
        cta.tNREngine = this.GetComponent<TNREngine>();

        taskEntryList[index].taskStationMarker = cta.markerArrow;
        taskEntryList[index].taskNumber.text = (index + 1).ToString();

        dialogueEngine.StartDialogue(ref failDialogue);
    }

    bool CheckIfUnique(int rnd)
    {
        for (int i = 0; i < ctaList.Length; i++)
        {
            if (ctaList[i] != null)
            {
                if (ctaList[i].transform.position == spawnPts[rnd].position)
                {
                    return false;
                }
            }
        }

        return true;
    }

    IEnumerator WaitForFadeIn()
    {
        sceneTransitions.Fade(true);
        while ((sceneTransitions.canvasGroup.alpha > 0.0f) && (sceneTransitions.canvas.enabled == true))
        {
            yield return null;
        }
        StaticItems.TNRstate++;
    }

    IEnumerator WaitForFadeOut()
    {
        sceneTransitions.Fade(false);
        while ((sceneTransitions.canvasGroup.alpha < 1.0f) && (sceneTransitions.canvas.enabled == true))
        {
            yield return null;
        }

        if (StaticItems.TNRstate == 19)
        {
            while (!Input.GetMouseButtonDown(0))
            {
                sceneTransitions.canvas.enabled = true;
                yield return null;
            }
        }

        StaticItems.TNRstate++;
    }
    #endregion
}
