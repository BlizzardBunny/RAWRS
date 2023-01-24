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
            toolSlider.value += 0.1f;
            stressSlider.value += 0.5f;
        }
    }

    private void Update()
    {
        if (image.alpha <= 0)
        {
            toolSlider.value = 0;
            Destroy(gameObject);
        }
    }
}
