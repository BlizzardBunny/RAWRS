using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSetup : MonoBehaviour
{
    #region Object References
    [SerializeField] private GameObject TheoState1, TheoState2;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 playerPos1, playerPos2;
    [SerializeField] private Canvas wasdCanvas, dialogueCanvas, pressECanvas, pressTabCanvas;
    [SerializeField] private NPCMovement nPCMovement;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (StaticItems.tutorialState == 1)
        {
            TheoState1.SetActive(true);
            TheoState2.SetActive(false);
            StaticItems.plrPos = playerPos1;
        }
        else if (StaticItems.tutorialState == 2)
        {
            dialogueCanvas.enabled = false;
            StartCoroutine(nPCMovement.StartMovement());
        }
        else if (StaticItems.tutorialState == 3)
        {
            TheoState1.SetActive(true);
            TheoState2.SetActive(false);
            TheoState1.transform.GetChild(0).gameObject.SetActive(true);
            StaticItems.plrPos = playerPos2;
            dialogueCanvas.enabled = false;
            pressECanvas.enabled = true;
        }
        else if (StaticItems.tutorialState == 4)
        {
            TheoState1.SetActive(true);
            TheoState2.SetActive(false);
            StaticItems.plrPos = playerPos2;
            dialogueCanvas.enabled = false;
            pressTabCanvas.enabled = true;
        }
        else if (StaticItems.tutorialState == 5)
        {
            TheoState1.SetActive(false);
            TheoState2.SetActive(true);
            StaticItems.plrPos = playerPos2;
        }
        player.transform.position = StaticItems.plrPos;
    }
    private void Update()
    {
        if (pressECanvas.enabled)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pressECanvas.enabled = false;
            }
        }

        if (pressTabCanvas.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                pressTabCanvas.enabled = false;
            }
        }
    }
}
