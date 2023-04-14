using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuEngine : MonoBehaviour
{
    #region Object References
    [Header("Panel Management")]
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Canvas optionsCanvas, creditsCanvas, confirmNewGamePanel;

    [Header("Main Menu")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button newGameButton, confirmNewGameButton, cancelNewGameButton, optionsButton, creditsButton, exitButton;

    [Header("Resolution")]
    [SerializeField] private Canvas confirmResolutionPanel;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Button acceptChanges, cancelChanges;
    [SerializeField] private TMPro.TextMeshProUGUI countdown;
    [SerializeField] private Toggle fullscreen;

    [Header("Return")]
    [SerializeField] private Button returnButtonOptions, returnButtonCredits;

    [Header("Scene Transitions")]
    [SerializeField] private SceneTransitions sceneTransitions;
    #endregion

    #region Variables
    private Canvas currCanvas;

    public static Vector2Int[] resolutions = { 
        new Vector2Int(720, 480), 
        new Vector2Int(1024, 720),
        new Vector2Int(1280, 1024),
        new Vector2Int(1366, 768),
        new Vector2Int(1600, 900),
        new Vector2Int(1920, 1080) };

    public static int prevResolution = -1;
    private Coroutine confirmCoroutine;
    private bool isCountingDown = false;
    #endregion


    void Start()
    {
        StaticItems.Reset(); //assures that the static items only follow the PlayerPrefs
        StaticItems.LoadGame();

        if (PlayerPrefs.GetString("playerName") == "")
        {
            FindClosestResolution();
        }

        optionsCanvas.enabled = false;
        creditsCanvas.enabled = false;
        confirmResolutionPanel.enabled = false;
        confirmNewGamePanel.enabled = false;

        currCanvas = mainCanvas;

        #region Add Listeners

        playButton.onClick.AddListener(PlayGame);

        newGameButton.onClick.AddListener(() => confirmNewGamePanel.enabled = true);
        confirmNewGameButton.onClick.AddListener(NewGame);
        cancelNewGameButton.onClick.AddListener(() => confirmNewGamePanel.enabled = false);

        optionsButton.onClick.AddListener(OpenOptions);
        creditsButton.onClick.AddListener(OpenCredits);
        exitButton.onClick.AddListener(ExitGame);

        resolutionDropdown.value = StaticItems.ResolutionIndex;
        fullscreen.isOn = StaticItems.Fullscreen;

        resolutionDropdown.onValueChanged.AddListener((i) => UpdateResolution(ref i));
        acceptChanges.onClick.AddListener(AcceptChanges);
        cancelChanges.onClick.AddListener(CancelChanges);
        fullscreen.onValueChanged.AddListener((bool b) =>
        {
            Screen.fullScreen = b;
            StaticItems.ResolutionIndex = resolutionDropdown.value;
            StaticItems.SaveGame();
        });

        returnButtonOptions.onClick.AddListener(ReturnToMainMenu);
        returnButtonCredits.onClick.AddListener(ReturnToMainMenu);

        #endregion

        if (StaticItems.levelNumber == 0 && StaticItems.tutorialState == 0)
        {
            playButton.gameObject.SetActive(false);
            newGameButton.onClick.RemoveAllListeners();
            newGameButton.onClick.AddListener(NewGame);
        }

        StaticItems.SaveGame();
    }

    private void Update()
    {

    }

    #region Resolution
    private void FindClosestResolution()
    {
        int currResolution = Screen.currentResolution.width;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (currResolution < resolutions[i].x)
            {
                if (i == 0)
                {
                    prevResolution = 0;
                }
                else
                {
                    prevResolution = i - 1;
                }
                resolutionDropdown.value = prevResolution;
                StaticItems.ResolutionIndex = resolutionDropdown.value;
                return;
            }
        }

        prevResolution = resolutions.Length - 1;
        resolutionDropdown.value = prevResolution;
        StaticItems.ResolutionIndex = resolutionDropdown.value;
        return;
    }

    private void UpdateResolution(ref int i)
    {
        Screen.SetResolution(resolutions[i].x, resolutions[i].y, StaticItems.Fullscreen);

        if (confirmCoroutine != null)
        {
            StopCoroutine(confirmCoroutine);
        }

        if (i != prevResolution)
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
        prevResolution = resolutionDropdown.value;
        isCountingDown = false;
        confirmResolutionPanel.enabled = false;
        StaticItems.ResolutionIndex = resolutionDropdown.value;
        StaticItems.SaveGame();
    }

    private void CancelChanges()
    {
        resolutionDropdown.value = prevResolution;
        isCountingDown = false;
        confirmResolutionPanel.enabled = false;
    }

    #endregion

    #region Panel Management

    private void ReturnToMainMenu()
    {
        currCanvas.enabled = !currCanvas.enabled;
        mainCanvas.enabled = true;
        currCanvas = mainCanvas;
    }

    private void OpenOptions()
    {
        currCanvas.enabled = !currCanvas.enabled;
        optionsCanvas.enabled = true;
        currCanvas = optionsCanvas;
    }

    private void OpenCredits()
    {
        currCanvas.enabled = !currCanvas.enabled;
        creditsCanvas.enabled = true;
        currCanvas = creditsCanvas;
    }

    #endregion

    private void NewGame()
    {
        StaticItems.Reset();
        StaticItems.SaveGame();

        PlayGame();
    }

    private void PlayGame()
    {
        if (StaticItems.inTutorial)
        {
            if (StaticItems.tutorialState == 0)
            {
                sceneTransitions.LoadScene("Intro");
            }
            else
            {
                sceneTransitions.LoadScene("Tutorial");
            }
        }
        else
        {
            if (StaticItems.levelNumber < 4)
            {
                sceneTransitions.LoadScene("Overworld");
            }
            else
            {
                sceneTransitions.LoadScene("TNR");
            }
        }
    }

    private void ExitGame()
    {
        //(Li) TODO: Confirm exit before exiting.
        StaticItems.ExitGame();
    }

}
