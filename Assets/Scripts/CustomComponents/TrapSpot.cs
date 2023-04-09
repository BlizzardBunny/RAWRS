using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpot : MonoBehaviour
{
    public Animator trapSpotAnim;
    public int direction;
    public CatTrappingArea parent;

    public void ConfirmTrap()
    {
        parent.ConfirmTrap(direction);
    }

    public void ReleaseCat()
    {
        trapSpotAnim.SetBool("isReleasing", true);
        TNREngine.completeTasks++;

        if (TNREngine.completeTasks == 3)
        {
            StartCoroutine(WaitForAnim());
        }
    }

    IEnumerator WaitForAnim()
    {
        yield return new WaitForSeconds(1.0f);
        TNREngine.completeTasks++;
    }
}
