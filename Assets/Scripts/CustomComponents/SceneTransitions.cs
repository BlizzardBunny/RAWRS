using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions: MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Canvas canvas;

    private bool isFading = false;
    private bool isFadingIn = true;
    private bool loadScene = false;
    private string sceneName = "";

    public float fadeSpeed = 0.75f;

    public static bool fadeOnStart = true;

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
        canvas.enabled = true;
        isFadingIn = inOut;
    }
    ///// <summary>
    ///// 
    ///// Fading In makes the referenced background disappear (fading into the scene).
    ///// Fading Out makes the referenced background reappear (fading out of the scene).
    ///// 
    ///// </summary>
    ///// <param name="inOut">set to TRUE for Fading In, FALSE for Fading Out</param>
    ///// <param name="fadeSpeed">rate of opacity lost/gained per frame</param>
    //public void Fade(bool inOut, float fadeSpeed)
    //{
    //    isFading = true;
    //    isFadingIn = inOut;
    //    this.fadeSpeed = fadeSpeed;
    //}

    public void LoadScene(string sceneName)
    {
        loadScene = true;
        this.sceneName = sceneName;
        Fade(false);
    }

    private void Start()
    {
        canvas.sortingOrder = 10;
        if (fadeOnStart)
        {
            Fade(true);
        }
        else
        {
            canvas.enabled = false;
            fadeOnStart = true;
        }
    }

    private void Update()
    {
        if (isFading)
        {
            if (isFadingIn)
            {
                canvasGroup.alpha -= fadeSpeed * Time.deltaTime;

                if (canvasGroup.alpha <= 0)
                {
                    if (loadScene)
                    {
                        loadScene = false;
                        SceneManager.LoadScene(sceneName);
                    }
                    isFading = false;
                }
            }
            else
            {
                canvasGroup.alpha += fadeSpeed * Time.deltaTime;

                if(canvasGroup.alpha >= 1) 
                {
                    if (loadScene)
                    {
                        loadScene = false;
                        SceneManager.LoadScene(sceneName);
                    }
                    isFading = false;
                }
            }
        }
        else
        {
            canvas.enabled = false;
        }
    }
}
