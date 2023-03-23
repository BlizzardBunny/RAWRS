using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region Object References

    [SerializeField] private Canvas pauseMenuCanvas, optionsCanvas;
    [SerializeField] private Button returnToMainMenu, exitGame, returnButton, saveButton, optionsButton;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
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
        SceneManager.LoadScene("MainMenu");
    }
    void OpenSettings()
    {        
        optionsCanvas.enabled = !optionsCanvas.enabled;
        pauseMenuCanvas.enabled = false;
        StaticItems.isShowingTasks = false;
    }
    void CloseSettings()
    {
        optionsCanvas.enabled = !optionsCanvas.enabled;
        pauseMenuCanvas.enabled = true;
        StaticItems.isShowingTasks = true;
    }

    void Save()
    {
        StaticItems.SaveGame();

        //IMPLEMENT: tell player game is saved
    }
}
