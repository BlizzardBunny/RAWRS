using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticItems
{
    //Items saved in PlayerPref
    #region Saved Items
    public static string playerName = "Player";
    public static int levelNumber = 0;
    public static bool init = true;
    public static bool isEnding = false;
    public static bool[] taskCompletion;
    public static List<System.Tuple<int, int>> entriesData = new List<System.Tuple<int, int>>();

    public static bool inTutorial = true;
    public static int tutorialState = 1;
    #endregion

    public static bool isPaused = false;
    public static bool isShowingTasks = false;

    public static float Volume = 1.0f;

    public static Vector3 plrPos = new Vector3(-5.5f, 6.5f, 0.0f);


    public static string[] petNames =
    {
        "Cat", "Dog"
    };

    public static bool firstTime = true; //remove when Saving system is implemented;

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
