using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectInteractionEngine : MonoBehaviour
{
    #region Object References

    [SerializeField] private Animator playerAnim;

    public Canvas dialogueCanvas;
    [SerializeField] private Image npcPic;
    [SerializeField] private TMPro.TextMeshProUGUI npcName;
    [SerializeField] private TMPro.TextMeshProUGUI npcDialogue;
    [SerializeField] private Button nextLine;

    #endregion

    #region Variables
    private GameObject obj;
    private TaskEngine taskEngine;
    private NPCDialogueInfo dialogueInfo;
    private int dialogueIndex = 1;

    //Directions
    private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
    private Vector3 left = new Vector3(-1.0f, 0.0f, 0.0f);
    private Vector3 down = new Vector3(0.0f, -1.0f, 0.0f);
    private Vector3 right = new Vector3(1.0f, 0.0f, 0.0f);

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        nextLine.onClick.AddListener(ContDialogue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckObject(RaycastHit2D hit)
    {      
        if (hit)
        {
            obj = hit.transform.gameObject;

            if (hit.transform.tag == "NPC")
            {
                if (!dialogueCanvas.enabled)
                {
                    StartDialogue();
                }
            }
            else if (hit.transform.tag == "TaskStation")
            {
                if (obj.gameObject.name == "BathingTaskStation")
                {
                    TaskEngine.taskType = 0;
                }
                else if (obj.gameObject.name == "FeedingTaskStation")
                {
                    TaskEngine.taskType = 1;
                }
                SceneManager.LoadScene("DBG_Tasks");
            }
        }
    }

    public void StartDialogue()
    {
        dialogueCanvas.enabled = true;

        dialogueInfo = obj.GetComponent<NPCDialogueInfo>();

        npcDialogue.text = dialogueInfo.dialogue[0];
        npcName.text = dialogueInfo.names[0];
        npcPic.sprite = dialogueInfo.sprites[0];
    }

    void ContDialogue()
    {
        if (dialogueIndex < dialogueInfo.dialogue.Length)
        {
            npcDialogue.text = dialogueInfo.dialogue[dialogueIndex];
            npcName.text = dialogueInfo.names[dialogueIndex];
            npcPic.sprite = dialogueInfo.sprites[dialogueIndex];

            dialogueIndex++;
        }
        else
        {
            dialogueIndex = 1;
            dialogueCanvas.enabled = false;
        }
    }
}
