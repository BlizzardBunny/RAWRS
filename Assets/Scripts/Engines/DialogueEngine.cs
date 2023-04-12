using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueEngine : MonoBehaviour
{
    #region Object References

    public Canvas dialogueCanvas;
    [SerializeField] private Image npcPic;
    [SerializeField] private TMPro.TextMeshProUGUI npcName;
    [SerializeField] private TMPro.TextMeshProUGUI npcDialogue;
    public Button nextLine;
    public SceneTransitions sceneTransitions;
    public NPCDialogueInfo nextStateDialogueInfo;

    #endregion

    #region Variables

    private NPCDialogueInfo dialogueInfo;
    private int dialogueIndex = 1;
    private GameObject child = null;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        dialogueCanvas.enabled = false;
        if (SceneManager.GetActiveScene().name != "Intro")
        {
            Init();
        }
    }

    public void Init()
    {
        nextLine.onClick.AddListener(ContDialogue);

        dialogueInfo = this.GetComponent<NPCDialogueInfo>();

        if (dialogueInfo != null)
        {
            if (dialogueInfo.playAtStart)
            {
                if (StaticItems.inTutorial)
                {
                    if (StaticItems.tutorialState != dialogueInfo.tutorialState)
                    {
                        if (StaticItems.tutorialState == nextStateDialogueInfo.tutorialState)
                        {
                            nextStateDialogueInfo.gameObject.SetActive(true);
                            dialogueInfo = nextStateDialogueInfo;
                        }
                    }

                    StartDialogue();
                }
                else if (dialogueInfo.tutorialState < 0)
                {
                    StartDialogue();
                }
            }
        }
        else
        {
            if (nextStateDialogueInfo != null)
            {
                if (nextStateDialogueInfo.playAtStart)
                {
                    nextStateDialogueInfo.gameObject.SetActive(true);
                    dialogueInfo = nextStateDialogueInfo;
                    StartDialogue();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueInfo != null && dialogueInfo.waitForInputAtEnd != "")
        {
            if (dialogueInfo.waitForInputAtEnd.ToUpper() == "TAB")
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    dialogueInfo.showAtEndCanvas.enabled = false;
                }
            }
        }
    }

    private string ApplyKeywords(ref string line)
    {
        if (line.Length == 0)
        {
            return line;
        }
        else
        {
            char[] chars = line.ToCharArray();
            line = "";

            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == '/')
                {
                    ++i;
                    if (chars[i] == 'p')
                    {
                        line += StaticItems.playerName;
                    }
                    else if (chars[i] == 'a')
                    {
                        ++i;
                        line += StaticItems.petNames[int.Parse(chars[i].ToString())];
                    }
                    else
                    {
                        line += "/" +  chars[i];
                    }
                }
                else
                {
                    line += chars[i];
                }
            }

            return line;
        }
    }

    public void StartDialogue(ref GameObject obj)
    {
        dialogueInfo = obj.GetComponent<NPCDialogueInfo>();

        if (dialogueInfo == null)
        {
            dialogueInfo = obj.transform.GetChild(0).GetComponent<NPCDialogueInfo>();
        }

        if (obj.transform.childCount > 0)
        {
            child = obj.transform.GetChild(0).gameObject;
        }

        if (dialogueInfo.playAtEndLevel)
        {
            if (StaticItems.isEnding)
            {
            }
            else
            {
                dialogueInfo = dialogueInfo.backupDialogue;
            }

            StartDialogue();
        }
        else
        {
            StartDialogue();
        }
    }

    public void StartDialogue()
    {
        if (StaticItems.inTutorial)
        {
            if (StaticItems.tutorialState <= 1 || StaticItems.tutorialState >= 5)
            {
                dialogueCanvas.enabled = true;
            }
        }
        else
        {
            dialogueCanvas.enabled = true;
        }

        if (dialogueInfo == null || dialogueInfo.dialogue.Length == 0 || dialogueInfo.names.Length == 0 || dialogueInfo.sprites.Length == 0)
        {
            Debug.LogError("Missing dialogue information");
            return;
        }

        npcDialogue.text = ApplyKeywords(ref dialogueInfo.dialogue[0]);
        npcName.text = dialogueInfo.names[0];
        npcPic.sprite = dialogueInfo.sprites[0];

        if (dialogueInfo.showAtStartCanvas != null)
        {
            dialogueInfo.showAtStartCanvas.enabled = true;
        }

        if (dialogueInfo.showAtEndCanvas != null)
        {
            dialogueInfo.showAtEndCanvas.enabled = false;
        }
    }

    private void ContDialogue()
    {
        if (dialogueInfo == null)
        {
            Debug.LogError("null");
        }

        if (dialogueInfo.showAtStartCanvas != null)
        {
            dialogueInfo.showAtStartCanvas.enabled = false;
        }

        if (dialogueIndex < dialogueInfo.dialogue.Length)
        {
            npcDialogue.text = ApplyKeywords(ref dialogueInfo.dialogue[dialogueIndex]);
            npcName.text = dialogueInfo.names[dialogueIndex];
            npcPic.sprite = dialogueInfo.sprites[dialogueIndex];

            dialogueIndex++;
        }
        else
        {
            dialogueIndex = 1;
            dialogueCanvas.enabled = false;

            if (StaticItems.inTutorial)
            {
                StaticItems.tutorialState = dialogueInfo.tutorialState + 1;
                if (StaticItems.tutorialState >= 5)
                {
                    StaticItems.inTutorial = false;
                }
            }
            else
            {
                if (!StaticItems.hasPlayed)
                {
                    StaticItems.hasPlayed = true;
                }
            }

            if (child != null && !child.name.Equals("Text (TMP)"))
            {
                child.SetActive(false);
            }

            if (dialogueInfo.showAtEndCanvas != null)
            {
                if (StaticItems.levelNumber < 4)
                {
                    dialogueInfo.showAtEndCanvas.enabled = true;
                    nextLine.interactable = false;
                }
                else
                {
                    StartCoroutine(WaitForFade());
                }
            }

            if (dialogueInfo.nextScene != "")
            {
                StaticItems.init = true;
                sceneTransitions.LoadScene(dialogueInfo.nextScene);
            }
            else if (dialogueInfo.NPCMovement != null)
            {
                if (dialogueInfo.NPCMovement.iter <= 0)
                {
                    dialogueInfo.NPCMovement.isMoving = true; 
                    StartCoroutine(dialogueInfo.NPCMovement.StartMovement());
                }
                else 
                {
                    dialogueInfo.NPCMovement.FaceEnd();
                }
            }

            if (StaticItems.levelNumber >= 4)
            {
                if ((StaticItems.TNRstate != 6) && (StaticItems.TNRstate != 23))
                {
                    StaticItems.TNRstate++;
                }
                else if (StaticItems.TNRstate == 6)
                {
                    if (TNREngine.completeTasks == 4)
                    {
                        StaticItems.TNRstate++;
                    }
                    else
                    {
                        if (dialogueInfo.names[0] == "Cara<size=50><br>(she/her)</size>"
                            && npcDialogue.text != "Looks like you've still got some cats to trap. Come back when you've trapped <u>3</u> cats.")
                        {
                            dialogueInfo.dialogue = new string[1];
                            dialogueInfo.dialogue[0] = "Looks like you've still got some cats to trap. Come back when you've trapped <u>3</u> cats.";

                            StartDialogue();

                            dialogueInfo.dialogue[0] = "Hi, /p! Are you done? Let's see...";
                        }
                    }
                }
            }
        }
    }

    #region Wait for Fades
    IEnumerator WaitForFade()
    {
        sceneTransitions.Fade(false);
        while ((sceneTransitions.canvasGroup.alpha < 1.0f) && (sceneTransitions.canvas.enabled == true))
        {
            yield return null;
        }
        dialogueInfo.showAtEndCanvas.enabled = true;

        sceneTransitions.Fade(true);
        while ((sceneTransitions.canvasGroup.alpha > 0.0f) && (sceneTransitions.canvas.enabled == true))
        {
            yield return null;
        }
    }

    #endregion

}
