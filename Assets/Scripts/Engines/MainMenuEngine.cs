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

    [Header("Main Menu")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton, exitButton;

    [Header("Resolution")]
    [SerializeField] private Canvas confirmPanel;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Button acceptChanges, cancelChanges;
    [SerializeField] private TMPro.TextMeshProUGUI countdown;

    [Header("Return")]
    [SerializeField] private Button returnButton;
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

    // Start is called before the first frame update
    void Start()
    {
        currCanvas = mainCanvas;

        #region Add Listeners

        playButton.onClick.AddListener(PlayGame);
        optionsButton.onClick.AddListener(OpenOptions);
        exitButton.onClick.AddListener(ExitGame);

        resolutionDropdown.onValueChanged.AddListener((i) => UpdateResolution(ref i));
        acceptChanges.onClick.AddListener(AcceptChanges);
        cancelChanges.onClick.AddListener(CancelChanges);

        returnButton.onClick.AddListener(ReturnToMainMenu);

        #endregion

        FindClosestResolution();
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
            confirmPanel.enabled = true;
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
        confirmPanel.enabled = false;
    }

    private void CancelChanges()
    {
        resolutionDropdown.value = prevResolution;
        isCountingDown = false;
        confirmPanel.enabled = false;
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

    #endregion

    private void PlayGame()
    {
        StaticItems.isPaused = false;
        StaticItems.plrPos = new Vector3(-0.5f, 3.5f, 0.0f);
        SceneManager.LoadScene("Overworld");
    }

    private void ExitGame()
    {
        //(Li) TODO: Confirm exit before exiting.
        StaticItems.ExitGame();
    }

}
