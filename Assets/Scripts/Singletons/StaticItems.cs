using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticItems
{
    //Items saved in PlayerPref
    #region Saved Items
    static public string playerName = "Player";
    public static int levelNumber = 0;

    static public bool inTutorial = true;
    static public int tutorialState = 1;
    #endregion

    static public bool isPaused = false;
    static public bool isShowingTasks = false;

    static public float Volume = 1.0f;

    static public Vector3 plrPos = new Vector3(-5.5f, 6.5f, 0.0f);


    static public string[] petNames =
    {
        "Cat", "Dog"
    };

    static public bool firstTime = true; //remove when Saving system is implemented;

    public static void ExitGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
