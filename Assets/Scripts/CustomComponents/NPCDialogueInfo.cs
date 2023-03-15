using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueInfo : MonoBehaviour
{
    public bool playAtEndLevel = false;
    public bool playAtStart;
    public int tutorialState = -1;
    public string nextScene;
    public NPCMovement NPCMovement;
    public Canvas showAtStartCanvas, showAtEndCanvas;
    public string waitForInputAtEnd = "";
    public NPCDialogueInfo backupDialogue;

    public string[] names;
    public Sprite[] sprites;
    public string[] dialogue;

    private void Awake()
    {
        if (playAtEndLevel)
        {
            if (nextScene == "")
            {
                nextScene = "Level End";
            }
        }        
    }
}
