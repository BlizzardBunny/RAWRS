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
    [SerializeField] private Vector2 endLocation;

    #endregion

    #region Variables

    public bool isMoving;
    public int iter = 0;
    private float distMoved = 0.0f;

    //Directions
    private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
    private Vector3 left = new Vector3(-1.0f, 0.0f, 0.0f);
    private Vector3 down = new Vector3(0.0f, -1.0f, 0.0f);
    private Vector3 right = new Vector3(1.0f, 0.0f, 0.0f);
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Collider2D>().enabled = !isMoving;
    }

    public void FaceEnd()
    {
        if (!isMoving)
        {
            NPCAnim.SetInteger("direction", endDirection);
        }
        else
        {
            Debug.Log(iter + " " + directions.Length);
            isMoving = true;
        }
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
            if (!isMoving)
            {
                iter--;
                yield return StartCoroutine(PauseMovement());
            }

            if (!StaticItems.isPaused)
            {
                NPCAnim.SetInteger("direction", directions[iter]);
                Vector3 startPos = transform.position;

                while (distMoved <= 1.0f)
                {
                    if (!isMoving)
                    {
                        iter--;
                        yield return StartCoroutine(PauseMovement());
                    }

                    if (directions[iter] == 0)
                    {
                        Move(startPos, new Vector3(0.0f, objRect.rect.height, 0.0f), up);
                    }
                    else if (directions[iter] == 1)
                    {
                        Move(startPos, new Vector3(-(objRect.rect.width), 0.0f, 0.0f), left);
                    }
                    else if (directions[iter] == 2)
                    {
                        Move(startPos, new Vector3(0.0f, -(objRect.rect.height), 0.0f), down);
                    }
                    else if (directions[iter] == 3)
                    {
                        Move(startPos, new Vector3(objRect.rect.width, 0.0f, 0.0f), right);
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

            yield return new WaitForEndOfFrame();
            iter++;

            if ((this.transform.position.x == endLocation.x)
                && (this.transform.position.y == endLocation.y))
            {
                break;
            }
        }

        if (this.transform.childCount > 0)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }

        isMoving = false;
        NPCAnim.SetInteger("direction", endDirection);
        NPCAnim.SetBool("isRunning", false);

        if (waitForInput)
        {
            yield return StartCoroutine(WaitForInput(KeyCode.E));
        }
    }

    void Move(Vector3 startPos, Vector3 moveDist, Vector2 direction)
    {
        Vector2 posV2 = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(posV2 + (direction * ((objRect.rect.width/2) + 0.01f)), direction, 0.1f);

        if (!hit)
        {
            float distToMove = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, startPos + moveDist, moveSpeed * Time.deltaTime);

            if (distMoved + distToMove > 1.0f)
            {
                transform.position = startPos + moveDist;
            }

            distMoved += distToMove;
        }
        else
        {
            distMoved = 1.5f;
        }
    }
}
