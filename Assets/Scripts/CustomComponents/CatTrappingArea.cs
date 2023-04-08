using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTrappingArea : MonoBehaviour
{
    public Animator animator;
    public RectTransform rect;

    private void Update()
    {
        if (StaticItems.TNRstate == 6)
        {
            if (HasEntered(StaticItems.plrPos))
            {
                ToggleUI(true);
            }
            else
            {
                ToggleUI(false);
            }
        }
    }

    private void ToggleUI(bool val)
    {
        animator.SetBool("isNear", val);
    }

    private bool HasEntered(Vector3 position)
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
}
