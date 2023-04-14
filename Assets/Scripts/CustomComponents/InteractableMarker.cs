using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMarker : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    public float searchDistance;
    public Collider2D collider;
    private Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.enabled = false;
        parent = transform.parent;
        collider = parent.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collider != null)
        {
            if (collider.enabled)
            {
                Appear();
            }
        }
        else
        {
            Appear();
        }
    }

    private void Appear()
    {
        if (Vector3.Distance(parent.position, StaticItems.plrPos) <= searchDistance && !StaticItems.isShowingDialogue)
        {
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }
}
