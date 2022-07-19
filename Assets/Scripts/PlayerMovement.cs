using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Object References

    [SerializeField] private Animator playerAnim;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAnim.SetInteger("direction", 0);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            playerAnim.SetInteger("direction", 1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnim.SetInteger("direction", 2);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            playerAnim.SetInteger("direction", 3);
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
}
