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

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerAnim.SetInteger("direction", 0);
            Move(up);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            playerAnim.SetInteger("direction", 1);
            Move(left);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerAnim.SetInteger("direction", 2);
            Move(down);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerAnim.SetInteger("direction", 3);
            Move(right);
        }

        if (!playerAnim.GetBool("isRunning"))
        {
            playerAnim.SetBool("isRunning", true);
        }

        if (!Input.anyKey)
        {
            playerAnim.SetBool("isRunning", false);
        }
    }

    void Move(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(direction), 0.5f);
        Debug.Log((bool)hit);
        Debug.DrawRay(transform.position, direction*0.5f, Color.red);

        if (!hit)
        {     
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, moveSpeed * Time.deltaTime);
        }
    }
}
