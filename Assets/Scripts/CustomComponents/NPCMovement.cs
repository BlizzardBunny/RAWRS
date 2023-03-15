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

    public bool isMoving;
    public int iter = 0;
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
            if (LevelEndEngine.levelNumber > 0)
            {
                NPCAnim.SetInteger("direction", startDirection);
            }
            else if (endLocation != null)
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

    private IEnumerator Pause()
    {
        NPCAnim.SetBool("isRunning", false);
        while (StaticItems.isPaused)
        {
            yield return null;
        }
        NPCAnim.SetBool("isRunning", true);
    }

    private IEnumerator PauseMovement()
    {
        while (!isMoving)
        {
            yield return null;
        }
    }

    public IEnumerator StartMovement()
    {
        if (waitForMovement)
        {
            yield return StartCoroutine(WaitForMovement());
        }

        isMoving = true;
        NPCAnim.SetBool("isRunning", true);
        while (iter < directions.Length)
        {
            if (!StaticItems.isPaused)
            {
                NPCAnim.SetInteger("direction", directions[iter]);
                Vector3 startPos = transform.position;

                while (distMoved <= 1.0f)
                {   
                    if (directions[iter] == 0)
                    {
                        Move(startPos, new Vector3(0.0f, objRect.rect.height, 0.0f));
                    }
                    else if (directions[iter] == 1)
                    {
                        Move(startPos, new Vector3(-(objRect.rect.width), 0.0f, 0.0f));
                    }
                    else if (directions[iter] == 2)
                    {
                        Move(startPos, new Vector3(0.0f, -(objRect.rect.height), 0.0f));
                    }
                    else if (directions[iter] == 3)
                    {
                        Move(startPos, new Vector3(objRect.rect.width, 0.0f, 0.0f));
                    }

                    yield return new WaitForEndOfFrame();
                }

                distMoved = 0.0f;
            }
            else
            {
                iter--;
                yield return StartCoroutine(Pause());
                isMoving = true;
            }

            if (!isMoving)
            {
                iter--;
                yield return StartCoroutine(PauseMovement());
            }

            yield return new WaitForEndOfFrame();
            iter++;
        }

        if (this.transform.childCount > 0)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }

        endLocation = this.transform.position;
        isMoving = false;
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
