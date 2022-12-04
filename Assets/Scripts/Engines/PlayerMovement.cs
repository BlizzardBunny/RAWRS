using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Object References

    [SerializeField] private Animator playerAnim;

    #endregion

    #region Variables
    public float moveSpeed = 5.0f;

    //Directions
    private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
    private Vector3 left = new Vector3(-1.0f, 0.0f, 0.0f);
    private Vector3 down = new Vector3(0.0f, -1.0f, 0.0f);
    private Vector3 right = new Vector3(1.0f, 0.0f, 0.0f);
    private Vector3 currDirection;

    public ObjectInteractionEngine oie;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        currDirection = up;
        playerAnim.SetInteger("direction", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!StaticItems.isPaused)
        {
            if (!oie.dialogueEngine.dialogueCanvas.enabled)
            {
                if (!playerAnim.GetBool("isRunning"))
                {
                    playerAnim.SetBool("isRunning", true);
                }

                if (Input.GetKey(KeyCode.W))
                {
                    playerAnim.SetInteger("direction", 0);
                    Move(up, 0.25f);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    playerAnim.SetInteger("direction", 1);
                    Move(left, 0.5f);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    playerAnim.SetInteger("direction", 2);
                    Move(down, 0.5f);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    playerAnim.SetInteger("direction", 3);
                    Move(right, 0.5f);
                }
                else
                {
                    playerAnim.SetBool("isRunning", false);
                }

                StaticItems.plrPos = this.transform.position;
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(currDirection), 0.5f);
                oie.CheckObject(hit);
            }
        }
    }

    void Move(Vector3 direction, float distance)
    {
        currDirection = direction;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(direction), distance);

        if (!hit)
        {     
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, moveSpeed * Time.deltaTime);
        }
    }
}
