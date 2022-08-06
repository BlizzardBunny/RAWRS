using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BowlState : MonoBehaviour, IPointerEnterHandler
{    
    [SerializeField] private int neededTool;
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private Canvas dirtCanvas;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (TaskEngine.tool == neededTool && !dirtCanvas.enabled)
        {            
            canvas.alpha -= 0.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas.alpha <= 0)
        {
            Destroy(gameObject);
        }
    }
}
