using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TaskEngine : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private Canvas endPanel;
    [SerializeField] private Canvas failPanel;
    [SerializeField] private Canvas bathCanvas, bathDirtspots;
    [SerializeField] private Canvas feedCanvas, foodDirtspots, foodSparkles;
    [SerializeField] private Canvas cleanCanvas, cleanDirtspots;
    [SerializeField] private Canvas checkupCanvas;

    [Header("States")]
    [SerializeField] private Transform bathStates;
    [SerializeField] private Transform feedStates;

    [Header("End Components")]
    [SerializeField] private Slider stressSlider;
    [SerializeField] private Button contBtn, retryBtn, exitBtn;
    [SerializeField] private Animator confettiAnim;
    public static int checkUpTasksTodo = 3;

    public static int tool;
    public static int taskType;

    private void EndScene()
    {
        SceneManager.LoadScene("DBG_Movement");
    }

    private void RetryScene()
    {
        checkUpTasksTodo = 3;
        SceneManager.LoadScene("DBG_Tasks");
    }

    private void Start()
    {
        endPanel.enabled = false;
        bathCanvas.enabled = false;
        feedCanvas.enabled = false;
        cleanCanvas.enabled = false;
        checkupCanvas.enabled = false;

        contBtn.onClick.AddListener(EndScene);
        retryBtn.onClick.AddListener(RetryScene);
        exitBtn.onClick.AddListener(EndScene);

        if (taskType == 0)
        {
            bathCanvas.enabled = true;
        }
        else if (taskType == 1)
        {
            feedCanvas.enabled = true;
        }
        else if (taskType == 2)
        {
            cleanCanvas.enabled = true;
        }
        else if (taskType == 3)
        {
            checkupCanvas.enabled = true;
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
        else if (taskType == 2)
        {
            if (cleanDirtspots.transform.childCount <= 0)
            {
                Cursor.visible = true;
                endPanel.enabled = true;
            }
        }
        else if (taskType == 3)
        {
            if (stressSlider.value >= 1)
            {
                Cursor.visible = true;
                failPanel.enabled = true;
            }
            else if (checkUpTasksTodo == 0)
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
