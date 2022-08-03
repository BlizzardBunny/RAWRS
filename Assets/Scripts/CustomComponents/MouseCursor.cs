using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseCursor : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image image;
    private bool isActive = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        isActive = true;
        Cursor.visible = false;
        image.raycastTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            transform.position = Input.mousePosition;

            if (Cursor.visible)
            {
                isActive = false;
            }
        }
    }
}
