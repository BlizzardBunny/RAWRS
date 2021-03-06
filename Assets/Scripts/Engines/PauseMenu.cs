using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region Object References

    [SerializeField] private Canvas pauseMenuCanvas;
    [SerializeField] private Button returnToMainMenu;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        returnToMainMenu.onClick.AddListener(ReturnToMain);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    void Pause()
    {
        StaticItems.isPaused = !StaticItems.isPaused;
        pauseMenuCanvas.enabled = !pauseMenuCanvas.enabled;
    }

    void ReturnToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
