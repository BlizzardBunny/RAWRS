using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TaskEngine : MonoBehaviour
{
    #region Object References
    [Header("Panels")]
    [SerializeField] private Canvas endPanel;
    [SerializeField] private Canvas failPanel;
    [SerializeField] private Canvas bathCanvas;
    [SerializeField] private Canvas feedCanvas, foodDirtspots, foodSparkles;
    [SerializeField] private Canvas cleanCanvas, cleanDirtspots;
    [SerializeField] private Canvas checkupCanvas;

    [Header("States")]
    [SerializeField] private Transform feedStates;

    [Header("End Components")]
    [SerializeField] private Slider bathStressSlider, checkupStressSlider;
    [SerializeField] private Button contBtn, retryBtn, exitBtn;
    [SerializeField] private Animator confettiAnim;

    [Header("Pet Specific Components")]
    [SerializeField] private Canvas bathingCat;
    [SerializeField] private Canvas bathingCatDirtSpots, bathingDog, bathingDogDirtspots, checkupCat, checkupDog;
    [SerializeField] private Transform bathCatStates, bathDogStates;
    [SerializeField] private Image cleaningPoopArea, feedingFoodBag, animalSprite;
    [SerializeField] private Sprite[] poopAreaSprites, foodBagSprites, animalSprites;
    #endregion

    #region Variables
    private Canvas bathDirtspots;
    private Transform bathStates;

    public static int checkUpTasksTodo = 3;

    public static int tool = -1;
    public static int taskType = 3;
    public static bool petType = true;
    public static int currStationID = -1;
    #endregion

    private void EndScene()
    {
        SceneTransitions.fadeOnStart = false;
        if (StaticItems.inTutorial)
        {
            StaticItems.tutorialState = 5;
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            SceneManager.LoadScene("Overworld");
        }
    }

    private void FailScene()
    {
        currStationID = -1;
        if (StaticItems.inTutorial)
        {
            StaticItems.tutorialState = 0;
        }
        EndScene();
    }

    private void RetryScene()
    {
        checkUpTasksTodo = 3;
        SceneManager.LoadScene("Tasks");
    }

    private void Start()
    {
        StaticItems.isShowingTasks = false;
        StaticItems.isPaused = false;

        failPanel.enabled = false;
        endPanel.enabled = false;
        bathCanvas.enabled = false;
        feedCanvas.enabled = false;
        cleanCanvas.enabled = false;
        checkupCanvas.enabled = false;
        bathingCatDirtSpots.enabled = false;
        bathingDogDirtspots.enabled = false;

        contBtn.onClick.AddListener(EndScene);
        retryBtn.onClick.AddListener(RetryScene);
        exitBtn.onClick.AddListener(FailScene);

        if (taskType == 0)
        {
            bathCanvas.enabled = true;

            if (petType)
            {
                bathingDog.enabled = true;
                bathingCat.enabled = false;
                bathDirtspots = bathingDogDirtspots;
                bathStates = bathDogStates;
            }
            else
            {
                bathingDog.enabled = false;
                bathingCat.enabled = true;
                bathDirtspots = bathingCatDirtSpots;
                bathStates = bathCatStates;
            }
        }
        else if (taskType == 1)
        {
            feedCanvas.enabled = true;

            if (petType)
            {
                feedingFoodBag.sprite = foodBagSprites[0];
                animalSprite.sprite = animalSprites[0];
            }
            else
            {
                feedingFoodBag.sprite = foodBagSprites[1];
                animalSprite.sprite = animalSprites[1];
            }
        }
        else if (taskType == 2)
        {
            cleanCanvas.enabled = true;

            if (petType)
            {
                cleaningPoopArea.sprite = poopAreaSprites[0];
            }
            else
            {
                cleaningPoopArea.sprite = poopAreaSprites[1];
            }
        }
        else if (taskType == 3)
        {
            checkupCanvas.enabled = true;

            if (petType)
            {
                checkupDog.enabled = true;
                checkupCat.enabled = false;
            }
            else
            {
                checkupDog.enabled = false;
                checkupCat.enabled = true;
            }
        }

        if (StaticItems.inTutorial)
        {
            exitBtn.interactable = false;
        }
    }

    private void Update()
    {
        if (taskType == 0)
        {
            if (bathStressSlider.value >= 1)
            {
                Cursor.visible = true;
                failPanel.enabled = true;
            }
            else if (bathStates.childCount <= 0)
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
            if (checkupStressSlider.value >= 1)
            {
                Cursor.visible = true;
                failPanel.enabled = true;
            }
            else if (checkUpTasksTodo == 0)
            {
                checkUpTasksTodo = 3;
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
