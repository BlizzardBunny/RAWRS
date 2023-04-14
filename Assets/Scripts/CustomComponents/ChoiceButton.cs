using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    public DialogueEngine dialogueEngine;
    public Canvas parent;

    private void Update()
    {
        if (parent.enabled)
        {
            if (!dialogueEngine.dialogueCanvas.enabled)
            {
                dialogueEngine.dialogueCanvas.enabled = true;
            }
        }
    }

    public void Play()
    {
        dialogueEngine.nextLine.interactable = true;
        parent.enabled = false;
        GameObject temp = this.gameObject;
        dialogueEngine.StartDialogue(ref temp);
    }
}
