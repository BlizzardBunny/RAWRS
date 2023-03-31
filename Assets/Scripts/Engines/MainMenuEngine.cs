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
    [SerializeField] private Canvas optionsCanvas;
    [SerializeField] private Canvas creditsCanvas;

    [Header("Main Menu")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button newGameButton, optionsButton, creditsButton, exitButton;

    [Header("New Game")]
    [SerializeField] private Canvas confirmNewGamePanel;
    [SerializeField] private Button acceptNewGame, cancelNewGame;

    [Header("Resolution")]
    [SerializeField] private Canvas confirmResolutionPanel;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Button acceptChanges, cancelChanges;
    [SerializeField] private TMPro.TextMeshProUGUI countdown;

    [Header("Return")]
    [SerializeField] private Button returnButtonOptions, returnButtonCredits;

    [Header("Scene Transitions")]
    [SerializeField] private SceneTransitions sceneTransitions;
    #endregion

    #region Variables
    private Canvas currCanvas;

    private Vector2Int[] resolutions = { 
        new Vector2Int(720, 480), 
        new Vector2Int(1024, 720),
        new Vector2Int(1280, 1024),
        new Vector2Int(1366, 768),
        new Vector2Int(1600, 900),
        new Vector2Int(1920, 1080) };

    private int prevResolution = -1;
    private int fullscreenIndex = 3;
    private Coroutine confirmCoroutine;
    private bool isCountingDown = false;
    #endregion


    void Start()
    {
        StaticItems.LoadGame();

        currCanvas = mainCanvas;

        if (StaticItems.playerName.Equals("Player"))
        {
            playButton.gameObject.SetActive(false);
        }

        #region Add Listeners

        playButton.onClick.AddListener(PlayGame);
        newGameButton.onClick.AddListener(ConfirmNewGame);
        optionsButton.onClick.AddListener(OpenOptions);
        creditsButton.onClick.AddListener(OpenCredits);
        exitButton.onClick.AddListener(ExitGame);

        acceptNewGame.onClick.AddListener(NewGame);
        cancelNewGame.onClick.AddListener(() => confirmNewGamePanel.enabled = false);

        resolutionDropdown.onValueChanged.AddListener((i) => UpdateResolution(ref i));
        acceptChanges.onClick.AddListener(AcceptChanges);
        cancelChanges.onClick.AddListener(CancelChanges);

        returnButtonOptions.onClick.AddListener(ReturnToMainMenu);
        returnButtonCredits.onClick.AddListener(ReturnToMainMenu);

        #endregion

        FindClosestResolution();
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
                return;
            }
        }

        prevResolution = resolutions.Length - 1;
        resolutionDropdown.value = prevResolution;
        return;
    }

    private void UpdateResolution(ref int i)
    {
        Screen.SetResolution(resolutions[i].x, resolutions[i].y, (FullScreenMode)fullscreenIndex);

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

    #region New/Play Game
    private void ConfirmNewGame()
    {
        if (StaticItems.playerName.Equals("Player"))
        {
            PlayGame();
        }
        else
        {
            confirmNewGamePanel.enabled = true;
        }
    }

    private void NewGame()
    {
        StaticItems.ResetGame();
        StaticItems.LoadGame();
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
            sceneTransitions.LoadScene("Overworld");
        }
    }
    #endregion

    private void ExitGame()
    {
        //(Li) TODO: Confirm exit before exiting.
        StaticItems.ExitGame();
    }

}
