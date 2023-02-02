using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] spriteRenderer;

    public void ToggleObj()
    {
        foreach (SpriteRenderer x in spriteRenderer)
        {
            x.enabled = !x.enabled;
        }
    }
}
