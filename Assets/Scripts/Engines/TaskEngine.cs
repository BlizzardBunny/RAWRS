using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskEngine : MonoBehaviour
{
    [SerializeField] private Canvas endPanel, bathCanvas, bathDirtspots, feedCanvas, foodDirtspots, foodSparkles;
    [SerializeField] private Transform bathStates, feedStates;
    public static int tool;
    public static int taskType;

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
                if (bathDirtspots.enabled)
                {
                    bathDirtspots.enabled = false;
                    Cursor.visible = true;
                }
            }
        }
        else if (taskType == 1)
        {
            if (foodDirtspots.transform.childCount <= 0)
            {
                if (foodDirtspots.enabled)
                {
                    foodDirtspots.enabled = false;
                    foodSparkles.enabled = false;
                    Cursor.visible = true;
                }
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
