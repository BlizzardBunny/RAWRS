using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DirtSpot : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private CanvasGroup image;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (TaskEngine.tool == 1)
        {
            image.alpha -= 0.1f;
        }        
    }

    private void Update()
    {
        if (image.alpha <= 0)
        {
            Destroy(gameObject);
        }
    }
}
