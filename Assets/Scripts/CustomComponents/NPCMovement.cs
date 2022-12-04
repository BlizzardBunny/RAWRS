using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    #region Object References

    [SerializeField] private Animator NPCAnim;
    [SerializeField] private int[] directions;
    [SerializeField] private NPCDialogueInfo dialogueInfo;

    public float moveSpeed = 5.0f;
    public int endDirection = 0;

    #endregion

    #region Variables

    //Directions
    private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
    private Vector3 left = new Vector3(-1.0f, 0.0f, 0.0f);
    private Vector3 down = new Vector3(0.0f, -1.0f, 0.0f);
    private Vector3 right = new Vector3(1.0f, 0.0f, 0.0f);
    private Vector3 currDirection;

    private float distMoved = 0.0f;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        int dir = NPCAnim.GetInteger("direction");

        if (dir  == 0)
        {
            currDirection = up;
        }
        else if (dir == 1)
        {
            currDirection = left;
        }
        else if (dir == 2)
        {
            currDirection = down;
        }
        else if (dir == 3)
        {
            currDirection = right;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator StartMovement()
    {
        dialogueInfo.isMoving = true;
        NPCAnim.SetBool("isRunning", true);
        for (int i = 0; i < directions.Length; i++)
        {
            if (!StaticItems.isPaused)
            {
                NPCAnim.SetInteger("direction", directions[i]);

                while (distMoved <= 1.0f)
                {   
                    if (directions[i] == 0)
                    {
                        Move(up);
                    }
                    else if (directions[i] == 1)
                    {
                        Move(left);
                    }
                    else if (directions[i] == 2)
                    {
                        Move(down);
                    }
                    else if (directions[i] == 3)
                    {
                        Move(right);
                    }

                    yield return new WaitForEndOfFrame();
                }

                distMoved = 0.0f;
            }

            yield return new WaitForEndOfFrame();
        }

        dialogueInfo.isMoving = false;
        NPCAnim.SetInteger("direction", endDirection);
        NPCAnim.SetBool("isRunning", false);
    }

    void Move(Vector3 direction)
    {
        currDirection = direction;
        float distToMove = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, distToMove);
        distMoved += distToMove;
    }
}
