
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSwitch : MonoBehaviour
{
    public int playAtLevel;
    [SerializeField] private DialogueEngine dialogueEngine;
    [SerializeField] private NPCDialogueInfo NPCDialogueInfo;
    [SerializeField] private GameObject prop;

    void Awake()
    {
        if (playAtLevel - 1 != StaticItems.levelNumber)
        {
            Destroy(prop.gameObject);
            Destroy(this.gameObject);
        }
        else
        {
            if (!StaticItems.hasPlayed)
            {
                prop.gameObject.SetActive(true);
                dialogueEngine.nextStateDialogueInfo = NPCDialogueInfo;
            }
        }
    }
}
