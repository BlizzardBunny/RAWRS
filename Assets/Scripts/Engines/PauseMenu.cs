using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    #region Object References

    [SerializeField] private SceneTransitions sceneTransitions;
    [SerializeField] private Canvas pauseMenuCanvas, optionsCanvas, confirmCanvas;
    [SerializeField] private Button returnToMainMenu, exitGame, returnButton, saveButton, optionsButton, confirmExitButton, cancelExitButton;
    [SerializeField] private Toggle fullscreen;
    [SerializeField] private Animator notifAnim;


    [Header("Resolution")]
    [SerializeField] private Canvas confirmResolutionPanel;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Button acceptChanges, cancelChanges;
    [SerializeField] private TMPro.TextMeshProUGUI countdown;

    #endregion

    #region Variables
    private Coroutine confirmCoroutine;
    private bool isCountingDown = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuCanvas.enabled = false;
        optionsCanvas.enabled = false;
        confirmCanvas.enabled = false;
        confirmResolutionPanel.enabled = false;

        returnToMainMenu.onClick.AddListener(() => confirmCanvas.enabled = true);
        returnToMainMenu.onClick.AddListener(() => confirmExitButton.onClick.AddListener(ReturnToMain));
        exitGame.onClick.AddListener(() => confirmCanvas.enabled = true);
        exitGame.onClick.AddListener(() => confirmExitButton.onClick.AddListener(StaticItems.ExitGame));

        resolutionDropdown.onValueChanged.AddListener((int i) => UpdateResolution(ref i));
        acceptChanges.onClick.AddListener(AcceptChanges);
        cancelChanges.onClick.AddListener(CancelChanges);
        fullscreen.onValueChanged.AddListener((bool b) =>
        {
            Screen.fullScreen = b;
            StaticItems.ResolutionIndex = resolutionDropdown.value;
            Save();
        });

        resolutionDropdown.value = StaticItems.ResolutionIndex;
        fullscreen.isOn = StaticItems.Fullscreen;

        cancelExitButton.onClick.AddListener(() => confirmCanvas.enabled = false);

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

    public void ReturnToMain()
    {
        StaticItems.isPaused = false;
        if (SceneManager.GetActiveScene().name == "EndingScene")
        {
            StaticItems.Reset();
        }
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

    void Save()
    {
        StaticItems.SaveGame();

        if (!notifAnim.GetBool("hasSaved"))
        {
            StartCoroutine(SendNotif(notifAnim));
        }
    }

    public static IEnumerator SendNotif(Animator notifAnim)
    {
        notifAnim.SetBool("hasSaved", true);
        yield return new WaitForSeconds(3f);
        notifAnim.SetBool("hasSaved", false);
    }

    #region Resolution

    private void UpdateResolution(ref int i)
    {
        Screen.SetResolution(MainMenuEngine.resolutions[i].x, MainMenuEngine.resolutions[i].y, StaticItems.Fullscreen);

        if (confirmCoroutine != null)
        {
            StopCoroutine(confirmCoroutine);
        }

        if (i != MainMenuEngine.prevResolution)
        {
            confirmResolutionPanel.enabled = true;
            isCountingDown = true;
            confirmCoroutine = StartCoroutine(Countdown());
        }
    }

    private IEnumerator Countdown()
    {
        for (int i = 10; i >= 0; i--)
        {
            if (isCountingDown)
            {
                countdown.text = i.ToString();

                yield return new WaitForSeconds(1);
            }
            else
            {
                yield return null;
            }
        }

        CancelChanges();
    }

    private void AcceptChanges()
    {
        MainMenuEngine.prevResolution = resolutionDropdown.value;
        isCountingDown = false;
        confirmResolutionPanel.enabled = false;
        StaticItems.ResolutionIndex = resolutionDropdown.value;

        Save();
    }

    private void CancelChanges()
    {
        resolutionDropdown.value = MainMenuEngine.prevResolution;
        isCountingDown = false;
        confirmResolutionPanel.enabled = false;
    }

    #endregion
}
