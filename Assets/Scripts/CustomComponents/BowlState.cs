using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BowlState : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{    
    [SerializeField] private int neededTool;
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private Canvas dirtCanvas;
    private bool isPouring;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (TaskEngine.tool == neededTool && !dirtCanvas.enabled)
        {            
            if (TaskEngine.tool == 4)
            {
                isPouring = true;
            }

            canvas.alpha -= 0.1f;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPouring = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPouring)
        {
            canvas.alpha -= 0.5f * Time.deltaTime;
        }

        if (canvas.alpha <= 0)
        {
            Destroy(gameObject);
        }
    }
}
