using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BowlState : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{    
    [SerializeField] private int neededTool;
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private Canvas dirtspots;
    [SerializeField] private Animator toolAnim;
    private bool isProcced;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (dirtspots != null)
        {
            if (TaskEngine.tool == neededTool && !dirtspots.enabled)
            {
                isProcced = true;
                toolAnim.SetBool("isInUse", true);
            }
        }
        else
        {
            if (TaskEngine.tool == neededTool)
            {
                isProcced = true;
                toolAnim.SetBool("isInUse", true);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isProcced = false;
        toolAnim.SetBool("isInUse", false);
    }

    private void Update()
    {
        if (isProcced)
        {
            canvas.alpha -= 0.5f * Time.deltaTime;
        }

        if (canvas.alpha <= 0)
        {
            toolAnim.SetBool("isInUse", false);
            
            if (this.name == "WaterBowl (1)" || this.name == "FoodBowl (1)")
            {
                Cursor.visible = true;
            }

            Destroy(gameObject);
        }
    }
}
