using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMarker : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    public float searchDistance;
    private Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.enabled = false;
        parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(parent.position, StaticItems.plrPos) <= searchDistance)
        {
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }
}
