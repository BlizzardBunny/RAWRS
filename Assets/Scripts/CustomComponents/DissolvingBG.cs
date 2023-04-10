using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DissolvingBG : MonoBehaviour
{
    public CanvasGroup thisCG, thatCG;
    public Image thisImage, bgImage;

    private void Start()
    {
        thisImage.color = bgImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (thisCG.alpha <= 0.01f)
        {
            Destroy(this.gameObject);
        }
        else
        {
            thisCG.alpha = thatCG.alpha;
        }
    }
}
