using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteractionEngine : MonoBehaviour
{
    #region Object References

    [SerializeField] private Animator playerAnim;

    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private Image npcPic;
    [SerializeField] private TMPro.TextMeshProUGUI npcName;
    [SerializeField] private TMPro.TextMeshProUGUI npcDialogue;

    #endregion

    #region Variables
    private GameObject obj;

    //Directions
    private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
    private Vector3 left = new Vector3(-1.0f, 0.0f, 0.0f);
    private Vector3 down = new Vector3(0.0f, -1.0f, 0.0f);
    private Vector3 right = new Vector3(1.0f, 0.0f, 0.0f);

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            CheckObject();
        }
    }

    void CheckObject()
    {
        RaycastHit2D hit;
        switch (playerAnim.GetInteger("direction"))
        {
            case 0:
                hit = Physics2D.Raycast(transform.position, transform.TransformDirection(up), 0.5f);
                break;
            case 1:
                hit = Physics2D.Raycast(transform.position, transform.TransformDirection(left), 0.5f);
                break;
            case 2:
                hit = Physics2D.Raycast(transform.position, transform.TransformDirection(down), 0.5f);
                break;
            case 3:
                hit = Physics2D.Raycast(transform.position, transform.TransformDirection(right), 0.5f);
                break;
            default:
                hit = Physics2D.Raycast(transform.position, transform.TransformDirection(down), 0.5f);
                break;
        }

        if (hit)
        {
            if (hit.transform.tag == "NPC")
            {
                RunDialogue();
            }
        }
    }

    void RunDialogue()
    {

    }
}
