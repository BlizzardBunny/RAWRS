using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSettingsEngine : MonoBehaviour
{
    #region Object References
    [SerializeField] private Canvas confirmPanel;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Button acceptChanges, cancelChanges;
    [SerializeField] private TMPro.TextMeshProUGUI countdown;
    #endregion

    #region Variables
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
        #region Add Listeners

        resolutionDropdown.onValueChanged.AddListener((i) => UpdateResolution(ref i));
        acceptChanges.onClick.AddListener(AcceptChanges);
        cancelChanges.onClick.AddListener(CancelChanges);

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

    //(Li) Delete this when using in other scenes outside DBG
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
