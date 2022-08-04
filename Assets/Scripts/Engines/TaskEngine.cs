using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskEngine : MonoBehaviour
{
    [SerializeField] private Canvas endPanel, dirtspots;
    [SerializeField] private Transform states;
    static public int tool;

    private void Update()
    {
        if (states.childCount <= 0)
        {
            Cursor.visible = true;
            endPanel.enabled = true;
        }

        if (dirtspots.transform.childCount <= 0)
        {
            dirtspots.enabled = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Cursor.visible = true;
        }
    }
}
