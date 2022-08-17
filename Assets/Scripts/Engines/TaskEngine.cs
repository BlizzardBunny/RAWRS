using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskEngine : MonoBehaviour
{
    [SerializeField] private int taskType;
    [SerializeField] private Canvas endPanel, bathCanvas, bathDirtspots, feedCanvas, foodDirtspots;
    [SerializeField] private Transform bathStates, feedStates;
    public static int tool;

    private void Start()
    {
        if (taskType == 0)
        {
            bathCanvas.enabled = true;
        }
        else if (taskType == 1)
        {
            feedCanvas.enabled = true;
        }
    }

    private void Update()
    {
        if (taskType == 0)
        {
            if (bathStates.childCount <= 0)
            {
                Cursor.visible = true;
                endPanel.enabled = true;
            }

            if (bathDirtspots.transform.childCount <= 0)
            {
                bathDirtspots.enabled = false;
            }
        }
        else if (taskType == 1)
        {
            if (foodDirtspots.transform.childCount <= 0)
            {
                foodDirtspots.enabled = false;
            }

            if (feedStates.childCount <= 0)
            {
                Cursor.visible = true;
                endPanel.enabled = true;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Cursor.visible = true;
        }
    }
}