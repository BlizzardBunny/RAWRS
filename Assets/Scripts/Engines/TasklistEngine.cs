using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasklistEngine : MonoBehaviour
{
    #region Object References

    [SerializeField] private Canvas tasklistCanvas;
    [SerializeField] private Transform taskBackground, pointA, pointB;

    #endregion

    #region Variables

    public float bgMoveSpeed = 5.0f;
    private bool isShowingTasks = false;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            ToggleTaskList();
        }

        if (isShowingTasks)
        {
            if (taskBackground.position != pointA.position)
            {
                taskBackground.position = Vector3.MoveTowards(taskBackground.position, pointA.position, bgMoveSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (taskBackground.position != pointB.position)
            {
                taskBackground.position = Vector3.MoveTowards(taskBackground.position, pointB.position, bgMoveSpeed * Time.deltaTime);
            }
            else
            {
                if (tasklistCanvas.enabled)
                {
                    tasklistCanvas.enabled = false;
                }
            }
        }
    }

    void ToggleTaskList()
    {
        if (!isShowingTasks)
        {
            tasklistCanvas.enabled = true;
            isShowingTasks = true;
        }
        else
        {
            isShowingTasks = false;
        }
    }

}
