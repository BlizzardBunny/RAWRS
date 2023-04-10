using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject failTrapDialogue;

    [SerializeField] private GameObject taskEntryPrefab;
    [SerializeField] private Transform taskEntryParent;

    public Canvas confirmCanvas;
    public Button confirmTask;
    [SerializeField] private Button cancelTask;

    [SerializeField] private GameObject caraMarker, releaseCats;
    #endregion

    #region Variables
    private GameObject[] ctaList = new GameObject[3]; //Length defines spawn number
    private TaskEntry[] taskEntryList = new TaskEntry[3];
    public static int completeTasks = 0;
    #endregion

    #region Unity Functions
    // Start is called before the first frame update
    void Start()
    {
        nextDay.enabled = false;
        confirmCanvas.enabled = false;
        caraMarker.SetActive(false);
        releaseCats.SetActive(false);
        completeTasks = 0;

        cancelTask.onClick.AddListener(() => confirmCanvas.enabled = false);
        StaticItems.LoadGame();
        StaticItems.levelNumber = 4;
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
            StaticItems.plrPos = new Vector3(109.5f, -6.5f, 0.0f);
            player.position = StaticItems.plrPos;
            playerAnim.SetInteger("direction", 1);
            dialogueEngine.dialogueCanvas.enabled = true;
            dialogueEngine.StartDialogue(ref dialogueStates[1]);
            SetupTrappingLevel();
        }
        else if (StaticItems.TNRstate == 6)
        {
            if (completeTasks == 3)
            {
                GameObject taskEntry = Instantiate(taskEntryPrefab, taskEntryParent);
                TaskEntry deets = taskEntry.GetComponent<TaskEntry>();
                deets.taskStationMarker = caraMarker;
                deets.taskName.text = "Talk to Cara";
                deets.taskNumber.text = "4";
                completeTasks++;
            }
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
            StaticItems.plrPos = new Vector3(109.5f, -6.5f, 0.0f);
            player.position = StaticItems.plrPos;
            playerAnim.SetInteger("direction", 1);
            dialogueEngine.dialogueCanvas.enabled = true;
            dialogueEngine.StartDialogue(ref dialogueStates[5]);
            completeTasks = 0;
            releaseCats.SetActive(true);
        }
        else if (StaticItems.TNRstate == 23)
        {
            if (completeTasks == 4)
            {
                StaticItems.TNRstate++;
            }
        }
        else if (StaticItems.TNRstate == 24)
        {
            dialogueEngine.dialogueCanvas.enabled = true;
            dialogueEngine.StartDialogue(ref dialogueStates[6]);
            StaticItems.TNRstate++;
        }
    }
    #endregion

    #region Trapping Functions
    void SetupTrappingLevel()
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

    public void MarkAsDone(int index)
    {
        if (taskEntryList[index].numberBG.color != Color.green)
        {
            taskEntryList[index].numberBG.color = Color.green;
            completeTasks++;
        }
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

        dialogueEngine.StartDialogue(ref failTrapDialogue);
    }
    #endregion

    #region Wait for Fades
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
