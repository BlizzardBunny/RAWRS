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

    public bool autoPlay;

    #endregion

    #region Variables

    private NPCDialogueInfo dialogueInfo;
    private int dialogueIndex = 1;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        nextLine.onClick.AddListener(ContDialogue);

        if (autoPlay)
        {
            dialogueInfo = this.GetComponent<NPCDialogueInfo>();
            StartDialogue();
        }
    }

    // Update is called once per frame
    void Update()
    {

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
                    else if (chars[i] == 'c')
                    {
                        line += StaticItems.petNames[0];
                    }
                    else if (chars[i] == 'd')
                    {
                        line += StaticItems.petNames[1];
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

        if (!dialogueInfo.isMoving)
        {
            StartDialogue();
        }
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
    }

    private void ContDialogue()
    {
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

            if (dialogueInfo.nextScene != "")
            {
                SceneManager.LoadScene(dialogueInfo.nextScene);
            }
            else if (dialogueInfo.NPCMovement != null)
            {
                StartCoroutine(dialogueInfo.NPCMovement.StartMovement());
            }
        }
    }
}
