using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectInteractionEngine : MonoBehaviour
{
    #region Object References

    public DialogueEngine dialogueEngine;

    [SerializeField] private Canvas confirmTaskCanvas;
    [SerializeField] private TMPro.TextMeshProUGUI confirmDialogue;
    [SerializeField] private Button confirmYes;
    [SerializeField] private Button confirmNo;

    #endregion

    #region Variables

    private GameObject obj;

    //Directions
    private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
    private Vector3 left = new Vector3(-1.0f, 0.0f, 0.0f);
    private Vector3 down = new Vector3(0.0f, -1.0f, 0.0f);
    private Vector3 right = new Vector3(1.0f, 0.0f, 0.0f);

    private TaskStationInfo taskStationInfo;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (confirmTaskCanvas != null)
        {
            confirmYes.onClick.AddListener(StartTask);
            confirmNo.onClick.AddListener(() => confirmTaskCanvas.enabled = false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckObject(RaycastHit2D hit, int plrDir)
    {      
        if (hit)
        {
            obj = hit.transform.gameObject;

            if (hit.transform.tag == "NPC")
            {
                if (!dialogueEngine.dialogueCanvas.enabled)
                {
                    NPCMovement NPCMovement = obj.GetComponent<NPCMovement>();

                    if (NPCMovement != null)
                    {
                        NPCMovement.isMoving = false;
                        if (plrDir <= 1)
                        {
                            NPCMovement.NPCAnim.SetInteger("direction", plrDir + 2);
                        }
                        else
                        {
                            NPCMovement.NPCAnim.SetInteger("direction", plrDir - 2);
                        }
                    }

                    dialogueEngine.dialogueCanvas.enabled = true;
                    dialogueEngine.StartDialogue(ref obj);
                }
            }
            else if (hit.transform.tag == "TaskStation")
            {
                taskStationInfo = obj.GetComponent<TaskStationInfo>();

                if (taskStationInfo.isActive)
                {
                    TaskEngine.taskType = taskStationInfo.taskType;
                    TaskEngine.petType = taskStationInfo.petType;
                    TaskEngine.currStationID = taskStationInfo.listID;
                    confirmDialogue.text = taskStationInfo.dialogue;

                    confirmTaskCanvas.enabled = true;
                }
            }
            else if (hit.transform.tag == "TrapSpot")
            {
                TrapSpot trapSpot = obj.GetComponent<TrapSpot>();

                trapSpot.ConfirmTrap();
            }
        }
    }   
    
    public void StartTask()
    {
        SceneManager.LoadScene("Tasks");
    }
}
