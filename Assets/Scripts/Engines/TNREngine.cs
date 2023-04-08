using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNREngine : MonoBehaviour
{
    #region Object References
    [SerializeField] private SceneTransitions sceneTransitions;
    [SerializeField] private TMPro.TextMeshProUGUI nextDay;
    [SerializeField] private Transform player;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private DialogueEngine dialogueEngine;
    public GameObject[] dialogueStates;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        nextDay.enabled = false;
        StaticItems.LoadGame();
        dialogueEngine.dialogueCanvas.enabled = true;
        dialogueEngine.StartDialogue(ref dialogueStates[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticItems.TNRstate == 1)
        {
            StaticItems.TNRstate++;
            StartCoroutine(WaitForFadeOut());
        }
        else if (StaticItems.TNRstate == 3)
        {
            StaticItems.TNRstate++;
            StartCoroutine(WaitForFadeIn()); 
            StaticItems.plrPos = new Vector3(110.5f, 1.5f, 0.0f);
            player.position = StaticItems.plrPos;
            playerAnim.SetInteger("direction", 1);
            dialogueEngine.dialogueCanvas.enabled = true;
            dialogueEngine.StartDialogue(ref dialogueStates[1]);
        }
        else if (StaticItems.TNRstate == 7)
        {
            dialogueEngine.dialogueCanvas.enabled = true;
            dialogueEngine.StartDialogue(ref dialogueStates[2]);
        }
        else if (StaticItems.TNRstate == 8)
        {
            StaticItems.TNRstate++;
            StartCoroutine(WaitForFadeOut());
        }
        else if (StaticItems.TNRstate == 10)
        {
            StaticItems.TNRstate++;
            StartCoroutine(WaitForFadeIn());
            StaticItems.plrPos = new Vector3(2.5f, 17.5f, 0.0f);
            player.position = StaticItems.plrPos;
            playerAnim.SetInteger("direction", 3);
            dialogueEngine.dialogueCanvas.enabled = true;
            dialogueEngine.StartDialogue(ref dialogueStates[3]);
        }
        else if (StaticItems.TNRstate == 13)
        {
            StaticItems.TNRstate++;
            StartCoroutine(WaitForFadeOut());
        }
        else if (StaticItems.TNRstate == 15)
        {
            StaticItems.TNRstate++;
            StartCoroutine(WaitForFadeIn());
            dialogueEngine.dialogueCanvas.enabled = true;
            dialogueEngine.StartDialogue(ref dialogueStates[4]);
        }
        else if (StaticItems.TNRstate == 18)
        {
            nextDay.enabled = true;
            StaticItems.TNRstate++;
            StartCoroutine(WaitForFadeOut());
        }
        else if (StaticItems.TNRstate == 20)
        {
            nextDay.enabled = false;
            StaticItems.TNRstate++;
            StartCoroutine(WaitForFadeIn());
            StaticItems.plrPos = new Vector3(110.5f, 1.5f, 0.0f);
            player.position = StaticItems.plrPos;
            playerAnim.SetInteger("direction", 1);
            dialogueEngine.dialogueCanvas.enabled = true;
            dialogueEngine.StartDialogue(ref dialogueStates[5]);
        }
        else if (StaticItems.TNRstate == 24)
        {
            dialogueEngine.dialogueCanvas.enabled = true;
            dialogueEngine.StartDialogue(ref dialogueStates[6]);
            StaticItems.TNRstate++;
        }
    }
    IEnumerator WaitForFadeIn()
    {
        sceneTransitions.Fade(true);
        while ((sceneTransitions.canvasGroup.alpha > 0.0f) && (sceneTransitions.canvas.enabled == true))
        {
            yield return null;
        }
        StaticItems.TNRstate++;
    }

    IEnumerator WaitForFadeOut()
    {
        sceneTransitions.Fade(false);
        while ((sceneTransitions.canvasGroup.alpha < 1.0f) && (sceneTransitions.canvas.enabled == true))
        {
            yield return null;
        }

        if (StaticItems.TNRstate == 19)
        {
            while (!Input.GetMouseButtonDown(0))
            {
                sceneTransitions.canvas.enabled = true;
                yield return null;
            }
        }

        StaticItems.TNRstate++;
    }
}
