using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePlayerName : MonoBehaviour
{
    [SerializeField] private CanvasGroup content, background;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button save;
    [SerializeField] private TMPro.TextMeshProUGUI question;
    [SerializeField] private TMPro.TMP_InputField inputName;
    [SerializeField] private DialogueEngine dialogueEngine;

    private float fadeGap = 0.5f; //amount of transparency lost per frame during fade
    private bool isFading = false;

    private void Start()
    {
        if (!StaticItems.playerName.Equals("Player"))
        {
            dialogueEngine.Init();
            content.alpha = 0.0f;
            isFading = true;
        }
        
        save.onClick.AddListener(UpdatePlayerName);
    }

    private void Update()
    {
        if (isFading) 
        {
            content.alpha -= fadeGap * Time.deltaTime;

            if (content.alpha <= 0 ) 
            {
                background.alpha -= fadeGap * Time.deltaTime;

                if (background.alpha <= 0)
                {
                    isFading = false;
                    content.alpha = 1.0f;
                    background.alpha = 1.0f;
                    canvas.enabled = false;
                }
            }
        }
    }

    public void UpdatePlayerName()
    {
        if (inputName.text.Length > 0)
        {
            StaticItems.playerName = inputName.text.Trim();            
            dialogueEngine.Init();
            isFading = true;
        }
        else
        {
            question.text = "What is your name? \n <color=red><size=70>(Do not leave blank)</size>";
        }
    }    
}
