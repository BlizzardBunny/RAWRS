using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DirtSpot : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private CanvasGroup image;
    [SerializeField] private Slider stressSlider, toolSlider;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (TaskEngine.tool == 1)
        {
            image.alpha -= 0.1f;
            if (toolSlider != null && stressSlider != null)
            {
                toolSlider.value += 0.1f;
                stressSlider.value += 0.05f;
            }
        }
    }

    private void Update()
    {
        if (image.alpha <= 0)
        {
            if (toolSlider != null)
            {
                toolSlider.value = 0;
            }
            Destroy(gameObject);
        }
    }
}
