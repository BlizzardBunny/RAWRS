
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSwitch : MonoBehaviour
{
    public int playAtLevel;

    // Start is called before the first frame update
    void Start()
    {
        if (playAtLevel != LevelEndEngine.levelNumber)
        {
            Destroy(this.gameObject);
        }
    }
}
