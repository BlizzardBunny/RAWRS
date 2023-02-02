using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticItems
{
    static public bool isPaused = false;
    static public bool inTutorial = true;
    static public bool isShowingTasks = false;

    static public int tutorialState = 1;

    static public int[] lvlTaskIDs;

    static public Vector3 plrPos = new Vector3(-5.5f, 6.5f, 0.0f);

    static public string playerName = "Player";

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
