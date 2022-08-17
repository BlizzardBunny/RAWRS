using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimalState : MonoBehaviour, IPointerEnterHandler
{    
    [SerializeField] private int neededTool;
    [SerializeField] CanvasGroup canvas;
    [SerializeField] Canvas dirtCanvas;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (TaskEngine.tool == neededTool && !dirtCanvas.enabled)
        {
            canvas.alpha -= 0.05f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas.alpha <= 0)
        {
            dirtCanvas.enabled = true;
            Cursor.visible = true;
            Destroy(gameObject);
        }
    }
}
