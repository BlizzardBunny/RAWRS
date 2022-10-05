using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BowlState : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{    
    [SerializeField] private int neededTool;
    [SerializeField] private bool isPouring;
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private Canvas dirtCanvas;
    [SerializeField] private Animation particleAnim;


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (TaskEngine.tool == neededTool && !dirtCanvas.enabled)
        {            
            if (!isPouring)
            {
                canvas.alpha -= 0.2f;
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isPouring)
        {
            //Queues each of these animations to be played one after the other
            particleAnim.PlayQueued("WaterCursor", QueueMode.CompleteOthers);
            particleAnim.PlayQueued("WaterBall", QueueMode.CompleteOthers);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas.alpha <= 0)
        {
            if (this.name == "FoodBowl (1)" || this.name == "WaterBowl (1)")
            {
                Cursor.visible = true;
            }
            Destroy(gameObject);
        }
    }
}
