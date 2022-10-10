using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BowlState : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{    
    [SerializeField] private int neededTool;
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private Animator toolAnim;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (TaskEngine.tool == neededTool)
        {
            toolAnim.SetBool("isInUse", true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolAnim.SetBool("isInUse", false);
    }

    private void Update()
    {
        if (toolAnim.GetBool("isInUse"))
        {
            canvas.alpha -= 0.5f * Time.deltaTime;
        }

        if (canvas.alpha <= 0)
        {
            toolAnim.SetBool("isInUse", false);
            Destroy(gameObject);
        }
    }
}
