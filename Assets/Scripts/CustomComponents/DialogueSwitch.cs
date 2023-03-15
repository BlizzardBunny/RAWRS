
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSwitch : MonoBehaviour
{
    public static bool hasPlayed = false;
    public int playAtLevel;
    [SerializeField] private DialogueEngine dialogueEngine;
    [SerializeField] private NPCDialogueInfo NPCDialogueInfo;
    [SerializeField] private GameObject prop;

    void Awake()
    {
        if (!hasPlayed)
        {
            if (playAtLevel - 1 != LevelEndEngine.levelNumber)
            {
                Destroy(this.gameObject);
            }
            else
            {
                prop.gameObject.SetActive(true);
                dialogueEngine.nextStateDialogueInfo = NPCDialogueInfo;
            }
            hasPlayed = true;
        }
    }
}
