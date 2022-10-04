using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TaskEngine : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private Canvas endPanel;
    [SerializeField] private Canvas bathCanvas, bathDirtspots;
    [SerializeField] private Canvas feedCanvas, foodDirtspots, foodSparkles;

    [Header("States")]
    [SerializeField] private Transform bathStates;
    [SerializeField] private Transform feedStates;

    [Header("End Components")]
    [SerializeField] private Button contBtn;
    [SerializeField] private Animator confettiAnim;

    public static int tool;
    public static int taskType;

    private void EndScene()
    {
        SceneManager.LoadScene("DBG_Movement");
    }

    private void Start()
    {
        endPanel.enabled = false;
        bathCanvas.enabled = false;
        feedCanvas.enabled = false;

        contBtn.onClick.AddListener(EndScene);

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

        confettiAnim.SetBool("atEnd", endPanel.enabled);

        if (Input.GetMouseButtonDown(1))
        {
            Cursor.visible = true;
        }
    }
}
