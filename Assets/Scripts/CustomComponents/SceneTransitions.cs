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
                    if (loadScene)
                    {
                        loadScene = false;
                        SceneManager.LoadScene(sceneName);
                    }

                    canvas.enabled = false;
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

                    canvas.enabled = false;
                    isFading = false;
                }
            }
        }
    }

}
