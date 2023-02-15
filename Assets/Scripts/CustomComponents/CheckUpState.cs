using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CheckUpState : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{    
    [SerializeField] private int neededTool;
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private Animator toolAnim;
    [SerializeField] private Slider stressSlider, toolSlider;
    private bool isProcced, isDestressing;
    private float stressLvl;
    private bool isActive = true;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (TaskEngine.tool == neededTool && isActive)
        {
            isProcced = true;
            toolAnim.SetBool("isInUse", true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (TaskEngine.tool != 12 && isActive)
        {
            canvas.alpha = 1;
        }
        else if (TaskEngine.tool == 12)
        {
            isDestressing = true;
            stressLvl = stressSlider.value;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canvas.alpha = 0;
        isProcced = false;
        isDestressing = false;
        toolAnim.SetBool("isInUse", false);
    }

    private void Update()
    {
        if (isActive && isProcced)
        {
            stressSlider.value += 0.075f * Time.deltaTime;
            toolSlider.value += 0.2f * Time.deltaTime;

            if (toolSlider.value >= 1)
            {
                Cursor.visible = true;
                canvas.alpha = 0;
                toolAnim.SetBool("isInUse", false);
                isProcced = false;
                isActive = false;
                TaskEngine.checkUpTasksTodo--;
            }
        }

        if (isDestressing)
        {
            stressSlider.value -= 0.2f * Time.deltaTime;

            if (stressLvl - stressSlider.value >= 0.05f)
            {
                isDestressing = false;
            }

            if (stressSlider.value == 0)
            {
                Cursor.visible = true;
            }
        }
    }
}
