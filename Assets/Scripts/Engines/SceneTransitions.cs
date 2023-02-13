using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitions: MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Canvas canvas;

    private bool isFading = false;
    private bool isFadingIn = true;

    public float fadeSpeed = 0.5f;

    /// <summary>
    /// 
    /// Fading In makes the referenced background disappear (fading into the scene).
    /// Fading Out makes the referenced background reappear (fading out of the scene).
    /// 
    /// </summary>
    /// <param name="inOut">set to TRUE for Fading In, FALSE for Fading Out</param>
    public void Fade(bool inOut)
    {
        isFading = true;
        isFadingIn = inOut;
    }
    /// <summary>
    /// 
    /// Fading In makes the referenced background disappear (fading into the scene).
    /// Fading Out makes the referenced background reappear (fading out of the scene).
    /// 
    /// </summary>
    /// <param name="inOut">set to TRUE for Fading In, FALSE for Fading Out</param>
    /// <param name="fadeSpeed">rate of opacity lost/gained per frame</param>
    public void Fade(bool inOut, float fadeSpeed)
    {
        isFading = true;
        isFadingIn = inOut;
        this.fadeSpeed = fadeSpeed;
    }

    private void Start()
    {
        Fade(true);
    }

    private void Update()
    {
        if (isFading)
        {
            canvas.enabled = true;
            if (isFadingIn)
            {
                canvasGroup.alpha -= fadeSpeed * Time.deltaTime;

                if (canvasGroup.alpha <= 0)
                {
                    canvasGroup.alpha = 1;
                    canvas.enabled = false;
                }
            }
            else
            {
                canvasGroup.alpha += fadeSpeed * Time.deltaTime;
            }
        }
    }

}
