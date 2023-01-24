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
    private bool isActive;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (TaskEngine.tool == neededTool && !dirtCanvas.enabled)
        {
            isActive = true;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            canvas.alpha -= 0.25f * Time.deltaTime;
            toolSlider.value = 1 - canvas.alpha;
            stressSlider.value += 0.75f * Time.deltaTime;
        }

        if (canvas.alpha <= 0)
        {
            dirtCanvas.enabled = true;
            Cursor.visible = true;
            Destroy(gameObject);
        }
    }
}
