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
    [SerializeField] private Animator waterAnim;
    private bool isPouring;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (TaskEngine.tool == neededTool && !dirtCanvas.enabled)
        {            
            if (TaskEngine.tool == 4)
            {
                isPouring = true;
                if (waterAnim != null)
                {
                    waterAnim.SetBool("isPouring", true);
                }
            }
            else
            {
                canvas.alpha -= 0.3f;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPouring = false;
        if (waterAnim != null)
        {
            waterAnim.SetBool("isPouring", false);
        }
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
            isPouring = false;
            if (waterAnim != null)
            {
                waterAnim.SetBool("isPouring", false);
            }
            Destroy(gameObject);
        }
    }
}
