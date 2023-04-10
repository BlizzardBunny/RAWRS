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
    private int currDirection = -1;
    private bool isDone = false;

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

            if (HasEntered(StaticItems.plrPos, nonoRect) && !isDone)
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

    public void ConfirmTrap(int direction)
    {
        currDirection = direction;
        tNREngine.confirmTask.onClick.AddListener(PlayAnim);
        tNREngine.confirmCanvas.enabled = true;
    }

    public void PlayAnim()
    {
        tNREngine.confirmCanvas.enabled = false;
        isDone = true;
        animator.SetInteger("direction", currDirection);
        animator.SetBool("isMoving", true);
        if (waitForAnim == null)
        {
            waitForAnim = StartCoroutine(WaitForPassAnim());
        }
    }

    IEnumerator WaitForPassAnim()
    {
        yield return new WaitForSeconds(5.66f);
        animator.SetBool("isMoving", false);
        tNREngine.MarkAsDone(index);
        markerArrow.SetActive(false);
        waitForAnim = null;
    }
}
