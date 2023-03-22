using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelEndEngine : MonoBehaviour
{
    #region Object References
    [SerializeField] Button nextLvl, restartLvl, endLvl;
    #endregion

    #region Variables
        
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        nextLvl.onClick.AddListener(NextLvl);
        restartLvl.onClick.AddListener(RestartLvl);
        endLvl.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
        StaticItems.plrPos = new Vector3(-5.5f, 6.5f, 0.0f);
        StopAllCoroutines();
    }

    private void NextLvl()
    {
        StaticItems.levelNumber++;
        DialogueSwitch.hasPlayed = false;
        LevelSetupEngine.ResetStaticVars();
        SceneManager.LoadScene("Overworld");
    }

    private void RestartLvl()
    {
        if (StaticItems.levelNumber == 0) 
        {
            StaticItems.inTutorial= true;
            StaticItems.tutorialState = 2;
            StaticItems.plrPos = new Vector3(-5.5f, 6.5f, 0.0f);
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            DialogueSwitch.hasPlayed = false;
            SceneManager.LoadScene("Overworld");
        }
    }
}
