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
    public static int tutorialState = 0;
    public static bool hasPlayed = false;

    public static int TNRstate = 0;
    #endregion

    #region Misc Items
    public static bool isPaused = false;
    public static bool isShowingTasks = false;

    public static float MasterVolume = 1.0f;
    public static float MusicVolume = 1.0f;
    public static float SFXVolume = 1.0f;

    public static Vector3 plrPos = new Vector3(-5.5f, 6.5f, 0.0f);


    public static string[] petNames =
    {
        "Cat", "Dog"
    };

    #endregion

    private static void SetSavedBool(string key, bool val)
    {
        if (val)
        {
            PlayerPrefs.SetInt(key, 1);
        }
        else
        {
            PlayerPrefs.SetInt(key, 0);
        }
    }

    public static void SaveGame()
    {
        PlayerPrefs.SetString("playerName", playerName);
        PlayerPrefs.SetInt("levelNumber", levelNumber);

        SetSavedBool("init", init);
        SetSavedBool("isEnding", isEnding);

        /*theoretically, with how taskCompletion is initialized in LevelSetupEngine.cs, the total size should be levelNumber + 1
        We use levelNumber + 1 here so that it's consistent in LoadGame()*/
        for (int i = 0; i < levelNumber + 1; i++)
        {
            SetSavedBool("taskCompletion" + i.ToString(), taskCompletion[i]);
        }

        //we need this for LoadGame() since entriesData is not a fixed size
        PlayerPrefs.SetInt("entriesDataCount", entriesData.Count);

        for (int i = 0; i < entriesData.Count; i++)
        {
            PlayerPrefs.SetInt("entry" + i.ToString() + "a", entriesData[i].Item1);
            PlayerPrefs.SetInt("entry" + i.ToString() + "b", entriesData[i].Item2);
        }

        SetSavedBool("inTutorial", inTutorial);
        PlayerPrefs.SetInt("tutorialState", tutorialState);
        SetSavedBool("hasPlayed", hasPlayed);

        PlayerPrefs.SetInt("TNRstate", TNRstate);

        PlayerPrefs.Save();
        Debug.Log("saved");
    }

    private static bool GetSavedBool(string key, int defVal)
    {
        return PlayerPrefs.GetInt(key, defVal) == 1;
    }

    public static void LoadGame()
    {
        playerName = PlayerPrefs.GetString("playerName", "Player");
        levelNumber = PlayerPrefs.GetInt("levelNumber", 0);

        init = GetSavedBool("init", 1);
        isEnding = GetSavedBool("isEnding", 0);

        taskCompletion = new bool[levelNumber + 1];
        for (int i = 0; i < levelNumber + 1; i++)
        {
            taskCompletion[i] = GetSavedBool("taskCompletion" + i.ToString(), 0);
        }

        int entriesDataCount = PlayerPrefs.GetInt("entriesDataCount", 0);
        entriesData.Clear();

        for (int i = 0; i < entriesDataCount; i++)
        {
            entriesData.Add
            (
                new System.Tuple<int, int> 
                (
                    PlayerPrefs.GetInt("entry" + i.ToString() + "a"), 
                    PlayerPrefs.GetInt("entry" + i.ToString() + "b")
                )
            );
        }

        inTutorial = GetSavedBool("inTutorial", 1);
        tutorialState = PlayerPrefs.GetInt("tutorialState", 0);
        hasPlayed = GetSavedBool("hasPlayed", 0);
        TNRstate = PlayerPrefs.GetInt("TNRstate", 0);
    }
    
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
