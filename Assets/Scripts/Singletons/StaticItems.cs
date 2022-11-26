using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticItems
{
    static public bool isPaused = false;

    static public Vector3 plrPos;

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
