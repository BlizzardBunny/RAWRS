using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PauseMenu : MonoBehaviour
{
    #region Object References

    [SerializeField] private Canvas pauseMenuCanvas, optionsCanvas, confirmPanel;
    [SerializeField] private Button returnToMainMenu, exitGame, returnButton, saveButton, optionsButton;
    [SerializeField] private SceneTransitions sceneTransitions;
    [SerializeField] private TMPro.TextMeshProUGUI confirmQuestion;
    [SerializeField] private Button confirmButton, cancelButton;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuCanvas.enabled = false;
        optionsCanvas.enabled = false;

        returnToMainMenu.onClick.AddListener(ReturnToMain);
        exitGame.onClick.AddListener(StaticItems.ExitGame);

        saveButton.onClick.AddListener(Save);
        optionsButton.onClick.AddListener(OpenSettings);
        returnButton.onClick.AddListener(CloseSettings);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (StaticItems.isShowingTasks)
            {
                StaticItems.isShowingTasks = false;
            }
            else
            {
                Pause();
            }
        }
        
    }

    void Pause()
    {
        StaticItems.isPaused = !StaticItems.isPaused;
        pauseMenuCanvas.enabled = StaticItems.isPaused;
    }

    void ReturnToMain()
    {
        StaticItems.isPaused = false;
        sceneTransitions.LoadScene("MainMenu");
    }
    void OpenSettings()
    {        
        optionsCanvas.enabled = !optionsCanvas.enabled;
        StaticItems.isShowingTasks = false;
    }
    void CloseSettings()
    {
        optionsCanvas.enabled = !optionsCanvas.enabled;
        pauseMenuCanvas.enabled = true;
        StaticItems.isShowingTasks = true;
    }

    void Confirm(bool type)
    {
        if (type) //return to main
        {
            confirmPanel.enabled = true;
            confirmQuestion.text = "Are you sure you want to return to main menu?";
            confirmButton.onClick.AddListener(ReturnToMain);
        }
        else //exit completely
        {

        }
    }

    void Save()
    {
        StaticItems.SaveGame();

        //IMPLEMENT: tell player game is saved
    }
}
