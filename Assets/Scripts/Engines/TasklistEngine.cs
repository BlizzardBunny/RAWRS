using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasklistEngine : MonoBehaviour
{
    #region Object References

    [SerializeField] private Canvas tasklistCanvas;
    [SerializeField] private Transform taskBackground, listPointA, listPointB, mapBackground, mapPointA, mapPointB;

    #endregion

    #region Variables

    public float listMoveSpeed = 5.0f;
    public float mapMoveSpeed = 5.0f;
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
            if (taskBackground.position != listPointA.position || mapBackground.position != mapPointA.position)
            {
                taskBackground.position = Vector3.MoveTowards(taskBackground.position, listPointA.position, listMoveSpeed * Time.deltaTime);
                mapBackground.position = Vector3.MoveTowards(mapBackground.position, mapPointA.position, mapMoveSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (taskBackground.position != listPointB.position || mapBackground.position != mapPointB.position)
            {
                taskBackground.position = Vector3.MoveTowards(taskBackground.position, listPointB.position, listMoveSpeed * Time.deltaTime);
                mapBackground.position = Vector3.MoveTowards(mapBackground.position, mapPointB.position, mapMoveSpeed * Time.deltaTime);
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
