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
    [SerializeField] private Button nextLine;
    [SerializeField] private NPCDialogueInfo nextStateDialogueInfo;

    #endregion

    #region Variables

    private NPCDialogueInfo dialogueInfo;
    private int dialogueIndex = 1;
    private GameObject child = null;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
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
                        nextStateDialogueInfo.gameObject.SetActive(true);
                        dialogueInfo = nextStateDialogueInfo;
                    }

                    StartDialogue();
                }
                else if (dialogueInfo.tutorialState < 0)
                {
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

        if (obj.transform.childCount > 0)
        {
            child = obj.transform.GetChild(0).gameObject;
        }

        StartDialogue();
    }

    public void StartDialogue()
    {
        dialogueCanvas.enabled = true;

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
    }

    private void ContDialogue()
    {
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

            if (StaticItems.inTutorial && dialogueInfo.tutorialState > 0)
            {
                StaticItems.tutorialState++;
                if (dialogueInfo.tutorialState == 3)
                {
                    StaticItems.inTutorial = false;
                }
            }

            if (child != null)
            {
                child.SetActive(false);
            }

            if (dialogueInfo.showAtEndCanvas != null)
            {
                dialogueInfo.showAtEndCanvas.enabled = true;
            }

            if (dialogueInfo.nextScene != "")
            {
                LevelSetupEngine.init = true;
                SceneManager.LoadScene(dialogueInfo.nextScene);
            }
            else if (dialogueInfo.NPCMovement != null)
            {
                StartCoroutine(dialogueInfo.NPCMovement.StartMovement());
            }
        }
    }


}
