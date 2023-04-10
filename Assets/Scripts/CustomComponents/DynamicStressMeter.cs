using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicStressMeter : MonoBehaviour
{
    public Sprite[] states;
    public Image image;
    public Slider stressMeter;
    public Canvas endPanel;

    // Start is called before the first frame update
    void Start()
    {
        image.sprite = states[0];
        stressMeter.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (stressMeter.value <= 0.5f)
        {
            image.sprite = states[0];
        }
        else if (stressMeter.value <= 0.75f)
        {
            image.sprite = states[1];
        }
        else
        {
            image.sprite = states[2];
        }

        if (endPanel.enabled)
        {
            image.sprite = states[0];
            stressMeter.value = 0;
        }
    }
}
