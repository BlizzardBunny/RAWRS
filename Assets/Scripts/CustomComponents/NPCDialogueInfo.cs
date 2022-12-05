using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueInfo : MonoBehaviour
{
    public bool playAtStart;
    public int tutorialState = -1;
    public bool isMoving;
    public string nextScene;
    public NPCMovement NPCMovement;

    public string[] names;
    public Sprite[] sprites;
    public string[] dialogue;
}
