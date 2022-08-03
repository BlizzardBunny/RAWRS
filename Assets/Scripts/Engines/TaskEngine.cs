using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskEngine : MonoBehaviour
{
    [SerializeField] private Canvas endPanel;
    [SerializeField] private Transform dirtspots;

    private void Update()
    {
        if (dirtspots.childCount <= 0)
        {
            Cursor.visible = true;
            endPanel.enabled = true;
        }
    }
}
