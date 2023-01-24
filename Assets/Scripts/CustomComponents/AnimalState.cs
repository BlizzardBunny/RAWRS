using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AnimalState : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{    
    [SerializeField] private int neededTool;
    [SerializeField] CanvasGroup canvas;
    [SerializeField] Canvas dirtCanvas;
    [SerializeField] Slider stressSlider, toolSlider;
    private bool isActive, isDestressing;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (TaskEngine.tool == neededTool && !dirtCanvas.enabled)
        {
            isActive = true;
        }
        else if (TaskEngine.tool == 12)
        {
            isDestressing = true;
        }
        else
        {
            isActive = false;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isActive = false;
        isDestressing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && !dirtCanvas.enabled)
        {
            canvas.alpha -= 0.25f * Time.deltaTime;
            toolSlider.value = 1 - canvas.alpha;
            stressSlider.value += 0.5f * Time.deltaTime;
        }
        else if (isDestressing)
        {
            stressSlider.value -= 0.2f * Time.deltaTime;
        }

        if (canvas.alpha <= 0)
        {
            dirtCanvas.enabled = true;
            Cursor.visible = true;
            Destroy(gameObject);
        }

        if (Cursor.visible)
        {
            isActive = false;
            isDestressing = false;
        }
    }
}
