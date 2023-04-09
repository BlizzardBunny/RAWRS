using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTrappingArea : MonoBehaviour
{
    public Animator animator;
    public RectTransform ctaRect, nonoRect;
    public GameObject markerArrow;
    public int index;
    public TNREngine tNREngine;

    private Coroutine waitForAnim = null;

    private void Update()
    {
        if (StaticItems.TNRstate == 6)
        {
            if (HasEntered(StaticItems.plrPos, ctaRect))
            {
                ToggleUI(true);
            }
            else
            {
                ToggleUI(false);
            }

            if (HasEntered(StaticItems.plrPos, nonoRect))
            {
                if (waitForAnim == null)
                {
                    waitForAnim = StartCoroutine(WaitForFailAnim());
                }
            }
        }
    }

    private void ToggleUI(bool val)
    {
        animator.SetBool("isNear", val);
    }

    IEnumerator WaitForFailAnim()
    {
        animator.SetBool("failing", true);
        yield return new WaitForSeconds(1.83f);
        waitForAnim = null;
        tNREngine.ReplaceCTAIndex(index);
        Destroy(this.gameObject);
    }

    public static bool HasEntered(Vector3 position, RectTransform rect)
    {
        if ((position.x > (rect.position.x - (rect.rect.width/2)) && position.x < (rect.position.x + (rect.rect.width / 2)))
            && (position.y > (rect.position.y - (rect.rect.height / 2)) && position.y < (rect.position.y + (rect.rect.height / 2))))
        {
            return true;
        }
        else
        {            
            return false;
        }
    }

    public void MarkAsDone()
    {
        tNREngine.MarkAsDone(index);
    }

    public void PlayAnim(int direction)
    {
        animator.SetInteger("direction", direction);
        animator.SetBool("isMoving", true);
        if (waitForAnim == null)
        {
            waitForAnim = StartCoroutine(WaitForPassAnim());
        }
    }

    IEnumerator WaitForPassAnim()
    {
        yield return new WaitForSeconds(5.66f);
        StopAnim();
        MarkAsDone();
        waitForAnim = null;
    }

    public void StopAnim()
    {
        animator.SetBool("isMoving", false);
    }
}
