using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseCursor : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int tool;
    [SerializeField] private Image image;
    [SerializeField] private Vector3 offset;
    [SerializeField] private string usagePhrase = ""; //for check-up task; phrase that displays on progress bar
    [SerializeField] private TMPro.TextMeshProUGUI sliderText;
    [SerializeField] private Slider slider;
    [SerializeField] private RightButtonEvent rightButtonEvent;
    private static int previousTool = -1;
    private bool isActive = false;
    private Vector2 startPos;

    public void OnPointerClick(PointerEventData eventData)
    {
        TaskEngine.tool = tool;

        isActive = true;
        image.raycastTarget = false;
        Cursor.visible = false;        

        if ((TaskEngine.taskType == 3 || TaskEngine.taskType == 0) && TaskEngine.tool != 12)
        {
            sliderText.text = usagePhrase;
            if (TaskEngine.tool != previousTool)
            {
                slider.value = 0;
            }
        }
    }

    private void Start()
    {
        startPos = transform.position;

        if (sliderText != null)
        {
            sliderText.text = "";
        }
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
                
                if (rightButtonEvent != null)
                {
                    rightButtonEvent.EndSound();
                }

                if (TaskEngine.tool != 12)
                {
                    previousTool = TaskEngine.tool;
                }
            }
        }
    }
}
