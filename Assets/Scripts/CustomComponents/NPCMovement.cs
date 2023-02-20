using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    #region Object References

    public float moveSpeed = 5.0f;
    public int startDirection = 0;
    public int endDirection = 0;
    public bool waitForMovement = false;
    public bool waitForInput = false;

    public Animator NPCAnim;
    [SerializeField] private RectTransform objRect;
    [SerializeField] private int[] directions;
    [SerializeField] private NPCDialogueInfo dialogueInfo;
    [SerializeField] private Canvas wasdCanvas;
    [SerializeField] private Canvas pressKeyCanvas;

    #endregion

    #region Variables

    private float distMoved = 0.0f;

    private static Vector2 endLocation;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (StaticItems.tutorialState == 2)
        {
            NPCAnim.SetInteger("direction", startDirection);
        }
        else
        {
            if (endLocation != null)
            {
                NPCAnim.SetInteger("direction", endDirection);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WaitForMovement()
    {
        wasdCanvas.enabled = true;
        while ((!Input.GetKeyDown(KeyCode.W)) 
            &&(!Input.GetKeyDown(KeyCode.A)) 
            &&(!Input.GetKeyDown(KeyCode.S)) 
            &&(!Input.GetKeyDown(KeyCode.D)))
        {
            yield return null;
        }
        wasdCanvas.enabled = false;
    }

    private IEnumerator WaitForInput(KeyCode key)
    {
        pressKeyCanvas.enabled = true;
        while (!Input.GetKeyDown(key))
        {
            yield return null;
        }
        pressKeyCanvas.enabled = false;
    }

    public IEnumerator StartMovement()
    {
        if (waitForMovement)
        {
            yield return StartCoroutine(WaitForMovement());
        }

        dialogueInfo.isMoving = true;
        NPCAnim.SetBool("isRunning", true);
        for (int i = 0; i < directions.Length; i++)
        {
            if (!StaticItems.isPaused)
            {
                NPCAnim.SetInteger("direction", directions[i]);
                Vector3 startPos = transform.position;
                Debug.Log(i);

                while (distMoved <= 1.0f)
                {   
                    if (directions[i] == 0)
                    {
                        Move(startPos, new Vector3(0.0f, objRect.rect.height, 0.0f));
                    }
                    else if (directions[i] == 1)
                    {
                        Move(startPos, new Vector3(-(objRect.rect.width), 0.0f, 0.0f));
                    }
                    else if (directions[i] == 2)
                    {
                        Move(startPos, new Vector3(0.0f, -(objRect.rect.height), 0.0f));
                    }
                    else if (directions[i] == 3)
                    {
                        Move(startPos, new Vector3(objRect.rect.width, 0.0f, 0.0f));
                    }

                    yield return new WaitForEndOfFrame();
                }

                distMoved = 0.0f;
            }

            yield return new WaitForEndOfFrame();
        }

        if (this.transform.childCount > 0)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }

        endLocation = this.transform.position;
        dialogueInfo.isMoving = false;
        NPCAnim.SetInteger("direction", endDirection);
        NPCAnim.SetBool("isRunning", false);

        if (waitForInput)
        {
            yield return StartCoroutine(WaitForInput(KeyCode.E));
        }
    }

    void Move(Vector3 startPos, Vector3 moveDist)
    {
        float distToMove = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, startPos + moveDist, moveSpeed * Time.deltaTime);

        if (distMoved + distToMove > 1.0f)
        {
            transform.position = startPos + moveDist;
        }

        distMoved += distToMove;
    }
}
