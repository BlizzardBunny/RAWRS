using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpot : MonoBehaviour
{
    public Animator animator;
    public int direction;

    private Coroutine waitForAnim = null;

    public void PlayAnim()
    {
        animator.SetInteger("direction", direction);
        animator.SetBool("isMoving", true);
        if (waitForAnim == null)
        {
            waitForAnim = StartCoroutine(WaitForAnim());
        }
    }

    IEnumerator WaitForAnim()
    {
        yield return new WaitForSeconds(5.66f);
        StopAnim();
        waitForAnim = null;
    }

    public void StopAnim()
    {
        animator.SetBool("isMoving", false);
    }
}
