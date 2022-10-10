using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseCursor : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    [SerializeField] private int tool;
    [SerializeField] private Image image;
    [SerializeField] private Vector3 offset;
    private bool isActive = false;
    private Vector2 startPos;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("ah");
        TaskEngine.tool = tool;

        isActive = true;
        image.raycastTarget = false;
        Cursor.visible = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("wah");
    }

    private void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            transform.position = Input.mousePosition + offset;

            if (Cursor.visible || TaskEngine.tool != tool)
            {
                transform.position = startPos;
                image.raycastTarget = true;
                isActive = false;
            }
        }
    }
}
