using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DirtSpot : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private CanvasGroup image;

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.alpha -= 0.05f;
    }

    private void Update()
    {
        if (image.alpha <= 0)
        {
            Destroy(gameObject);
        }
    }
}
