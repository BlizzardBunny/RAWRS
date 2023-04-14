using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    #region Object References

    [SerializeField] private Animator playerAnim;
    [SerializeField] private RectTransform body;

    #endregion

    #region Variables
    public float moveSpeed = 5.0f;

    //Directions
    private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
    private Vector3 left = new Vector3(-1.0f, 0.0f, 0.0f);
    private Vector3 down = new Vector3(0.0f, -1.0f, 0.0f);
    private Vector3 right = new Vector3(1.0f, 0.0f, 0.0f);
    private Vector3 currDirection;
    private Vector3 halfheight, halfwidth;
    private float padding = 0.4f; //padding when/where collisions are checked.

    public ObjectInteractionEngine oie;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        halfheight = new Vector3(0f, (body.rect.height - padding) / 2 , 0f);
        halfwidth = new Vector3((body.rect.width - padding) / 2, 0f, 0f);
        currDirection = up;
        playerAnim.SetInteger("direction", 0);
        this.transform.position = StaticItems.plrPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (!StaticItems.isPaused)
        {
            if (!StaticItems.isShowingDialogue && !StaticItems.isShowingTasks)
            {
                if (!playerAnim.GetBool("isRunning"))
                {
                    playerAnim.SetBool("isRunning", true);
                }

                if (Input.GetKey(KeyCode.W))
                {
                    playerAnim.SetInteger("direction", 0);
                    Move(up, 0.5f);
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
                oie.CheckObject(hit, playerAnim.GetInteger("direction"));
            }
        }
    }

    void Move(Vector3 direction, float distance)
    {
        currDirection = direction;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(direction), distance);
        Debug.DrawRay(transform.position, transform.TransformDirection(direction), Color.red, 0.25f);

        if (!hit)
        {
            RaycastHit2D hit1, hit2;
            if (direction == left || direction == right)
            {
                hit1 = Physics2D.Raycast(transform.position + halfheight, transform.TransformDirection(direction), distance);
                hit2 = Physics2D.Raycast(transform.position - halfheight, transform.TransformDirection(direction), distance);
                Debug.DrawRay(transform.position + halfheight, transform.TransformDirection(direction), Color.red, 0.25f);
                Debug.DrawRay(transform.position - halfheight, transform.TransformDirection(direction), Color.red, 0.25f);
            }
            else
            {
                hit1 = Physics2D.Raycast(transform.position + halfwidth, transform.TransformDirection(direction), distance);
                hit2 = Physics2D.Raycast(transform.position - halfwidth, transform.TransformDirection(direction), distance);
                Debug.DrawRay(transform.position + halfwidth, transform.TransformDirection(direction), Color.red, 0.25f);
                Debug.DrawRay(transform.position - halfwidth, transform.TransformDirection(direction), Color.red, 0.25f);
            }

            if (!hit1 && !hit2)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, moveSpeed * Time.deltaTime);
            }
            else if (hit1)
            {
                if (hit1.transform.tag.Equals("NPC"))
                {
                    CheckIfStuck(hit1);
                }
            }
            else if (hit2)
            {
                if (hit2.transform.tag.Equals("NPC"))
                {
                    CheckIfStuck(hit2);
                }
            }
        }
        else
        {
            if (hit.transform.tag.Equals("NPC"))
            {
                CheckIfStuck(hit);
            }
        }
    }

    private void CheckIfStuck(RaycastHit2D hit)
    {

        //RectTransform obj = hit.transform.GetComponent<RectTransform>();

        //if (obj == null)
        //{
        //    return;
        //}

        //if ((StaticItems.plrPos.x > obj.position.x - (obj.rect.width / 2)) 
        //    && (StaticItems.plrPos.x < obj.position.x + (obj.rect.width / 2))
        //    && (StaticItems.plrPos.y > obj.position.y - (obj.rect.height / 2))
        //    && (StaticItems.plrPos.y < obj.position.y + (obj.rect.height / 2)))
        //{
            /*IMPLEMENT: Find nearest clear space and teleport player to it*/
        //    transform.position = StaticItems.plrPos - new Vector3(currDirection.x * obj.rect.width, currDirection.y * obj.rect.height, 0.0f);
        //}
    }
}
